use BookStore

--Create
create table Books
(
	BookId int Identity(1,1) primary key,
	Title varchar(100),
	Author varchar(100),
	Rating float,
	RatingCount int,
	Price decimal(6, 2),
	OriginalPrice decimal(6, 2),
	DiscountPercentage float,
	Description varchar(max),
	Image varchar(max),
	Quantity int
)

select * from Books

--Add_Book
create or alter procedure Add_Book_SP
(
    @Title varchar(100),
    @Author varchar(100),
    @OriginalPrice decimal(6, 2),
	@DiscountPercentage float,
    @Description varchar(max),
    @Image varchar(max),
    @Quantity int
)
as 
begin
	SET NOCOUNT ON;
	begin try
		-- Check if any parameter is NULL or not valid
        if @Title Is null or 
           @Author  Is null or 
           @OriginalPrice  Is null or 
           @DiscountPercentage Is null or 
           @Description  Is null or 
           @Image  Is null or 
           @Quantity  Is null or 
           @OriginalPrice < 0 or
           @DiscountPercentage < 0 or @DiscountPercentage > 100 or
           @Quantity < 0
        begin
            RAISERROR('All parameters must be provided and valid.', 16, 1);
            return
        end

		declare @BookId int;

		-- Insert the book details into the Books table
		insert into Books (Title, Author, OriginalPrice, DiscountPercentage, Description, Image, Quantity)
		values (@Title, @Author, @OriginalPrice, @DiscountPercentage, @Description, @Image, @Quantity);

		-- Check if the record was inserted successfully
		if @@ROWCOUNT = 1
        begin
            print 'Book inserted successfully.';
        end
        else
        begin
            RAISERROR('Error inserting Book record.', 16, 1);
            return
        end

		-- Get the ID of the newly inserted book
		set @BookId = SCOPE_IDENTITY();

		-- Update the discounted price of the book
		update Books
		set Price = OriginalPrice - OriginalPrice *(@DiscountPercentage/100)
		where BookId = @BookId;

		 -- Check if the update was successful
        if @@ROWCOUNT = 1
        begin
            print 'Price updated successfully.';
        end
        ELSE
        begin
            RAISERROR('Error updating Price.', 16, 1);
            return
        end
	end try
	begin catch
        declare @ErrorMessage NVARCHAR(4000);
        declare @ErrorSeverity INT;
        declare @ErrorState INT;

        select @ErrorMessage = ERROR_MESSAGE(), 
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    end catch
end

exec Add_Book_SP
		@Title = 'To Kill a Mockingbird',
		@Author = 'Harper Lee',
		@OriginalPrice = 15.00,
		@DiscountPercentage = 5,
		@Description = 'A novel about the serious issues of rape and racial inequality.',
		@Image = 'mockingbird_image_path.jpg',
		@Quantity = 10

--GetAll_Books
create or alter procedure Get_AllBooks_SP
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the Book exists
		if exists (select 1 from Books)
			begin
				select * from Books
			end
		else
			begin 
				RAISERROR('No Books found.', 16, 1);
				return
			end
	end try
	begin catch
        declare @ErrorMessage NVARCHAR(4000);
        declare @ErrorSeverity INT;
        declare @ErrorState INT;

        select @ErrorMessage = ERROR_MESSAGE(), 
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    end catch
end

exec Get_AllBooks_SP

--Update_Book
create or alter procedure Update_Book_SP
(
    @BookId int,
    @Title varchar(100),
    @Author varchar(100),
    @OriginalPrice decimal(6, 2),
    @DiscountPercentage float,
    @Description varchar(max),
    @Image varchar(max),
    @Quantity int
)
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the Book exists
		if exists (select 1 from Books where BookId=@BookId)
		begin
			-- Update the Book's details
			update Books
			set Title = @Title,
				Author = @Author,
				OriginalPrice = @OriginalPrice,
				DiscountPercentage = @DiscountPercentage,
				Description = @Description,
				Image = @Image,
				Quantity = @Quantity,
				Price = @OriginalPrice - @OriginalPrice * (@DiscountPercentage / 100)
			where BookId = @BookId

			-- Check if any rows were affected by the update
			if @@ROWCOUNT = 1
			begin
				print 'Book details updated successfully.'
			end
			else
			begin
				RAISERROR('Book details could not be updated.', 16, 1);
				return
			end
		end
		else
			begin
				RAISERROR('Book not found.', 16, 1);
				return
			end
	end try
	begin catch
        declare @ErrorMessage NVARCHAR(4000);
        declare @ErrorSeverity INT;
        declare @ErrorState INT;

        select @ErrorMessage = ERROR_MESSAGE(), 
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    end catch
end

exec Update_Book_SP
		@BookId = 4,
		@Title = 'The Catcher in the Rye',
		@Author = 'J.D. Salinger',
		@OriginalPrice = 16.00,
		@DiscountPercentage = 10,
		@Description = 'A coming-of-age novel that explores themes of teenage angst and alienation.',
		@Image = 'catcher_in_the_rye_image_path.jpg',
		@Quantity = 80;

--Delete_Book
create or alter procedure Delete_Book_SP
@BookId int
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the Book exists
		if exists (select 1 from Books where BookId=@BookId)
		begin
			 -- Delete the Book
			 delete from Books where BookId = @BookId

			  -- Check if any rows were affected by the delete operation
			  if @@ROWCOUNT = 1
			  begin
				 print 'Book deleted successfully.'
			  end
			  else
			  begin
				RAISERROR('Book could not be deleted.', 16, 1);
				return
			  end
		end
		else
			begin
				RAISERROR('Book not found.', 16, 1);
				return
			end
	end try
	begin catch
        declare @ErrorMessage NVARCHAR(4000);
        declare @ErrorSeverity INT;
        declare @ErrorState INT;

        select @ErrorMessage = ERROR_MESSAGE(), 
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    end catch
end

exec Delete_Book_SP
		@BookId=7

--GetBook_ByBookId
create or alter procedure GetBook_ByBookId_SP
@BookId int
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the Book exists
		if exists (select 1 from Books where BookId=@BookId)
			begin
				select * from Books where BookId = @BookId
			end
		else
			begin 
				RAISERROR('Book not found.', 16, 1);
				return
			end
	end try
	begin catch
        declare @ErrorMessage NVARCHAR(4000);
        declare @ErrorSeverity INT;
        declare @ErrorState INT;

        select @ErrorMessage = ERROR_MESSAGE(), 
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    end catch
end

exec GetBook_ByBookId_SP 
		@BookId=1

select * from Users
select * from Books
select * from Feedbacks