use BookStore

--Create
create table Wishlist
(
    WishlistId int Identity(1,1) primary key,
	UserId int foreign key references Users(UserId),
	BookId int foreign key references Books(BookId),
	Title varchar(100),
	Author varchar(100),
	Image varchar(max),
	Price decimal(10, 2),
	OriginalPrice decimal(10, 2)
)

select * from Wishlist

--AddBook_ToWishlist
create or alter procedure AddBook_ToWishlist_SP
@UserId int,
@BookId int
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if any parameter is NULL
        if @UserId Is null or 
		   @BookId Is null
        begin
            RAISERROR('Both UserId and BookId must be provided.', 16, 1);
            return
        end

		declare @Title varchar(50),
				@Author varchar(50),
				@Image varchar(max),
				@Price decimal(10, 2), 
				@OriginalPrice decimal(10, 2)
	
		-- Get the details from the book
		select @Title = Title,
			   @Author = Author,
			   @Image = Image,
			   @Price = Price, 
			   @OriginalPrice = OriginalPrice
		from Books 
		where BookId = @BookId

		-- Check if the book exists in the Books table
		if @@ROWCOUNT > 0
		begin
			-- Insert the book into the Wishlist table
			insert into Wishlist (UserId, BookId, Title, Author, Image, Price, OriginalPrice)
			values (@UserId, @BookId, @Title, @Author, @Image, @Price, @OriginalPrice);
        
			print 'Book added to wishlist successfully.';
		end
		else
		begin
			RAISERROR ('Book not found.', 16, 1);
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

exec AddBook_ToWishlist_SP  @UserId=4, 
							@BookId=2
	
--View_Wishlist
create or alter procedure View_Wishlist_SP
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the Wishlist exists
		if exists (select 1 from Wishlist)
			begin
				select * from Wishlist
			end
		else
			begin 
				RAISERROR('Empty Wishlist', 16, 1);
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

exec View_Wishlist_SP 

--RemoveBook_FromWishlist
create or alter procedure RemoveBook_FromWishlist_SP
@WishlistId int
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the Wishlist item exists
		if exists (select 1 from Wishlist where WishlistId=@WishlistId)
		begin
			 -- Delete the Wishlist item
			 delete from Wishlist where WishlistId = @WishlistId

			  -- Check if any rows were affected by the delete operation
			  if @@ROWCOUNT = 1
			  begin
				 print 'Wishlist item deleted successfully.'
			  end
			  else
			  begin
				RAISERROR('Wishlist item could not be deleted.', 16, 1);
				return
			  end
		end
		else
			begin
				RAISERROR('Wishlist item not found.', 16, 1);
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

exec RemoveBook_FromWishlist_SP @WishlistId=2

--View_Wishlist_ByUserId
create or alter procedure View_Wishlist_ByUserId_SP
@UserId int
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the Wishlist item exists
		if exists (select 1 from Wishlist where UserId = @UserId)
			begin
				select * from Wishlist where UserId = @UserId
			end
		else
			begin 
				RAISERROR('No Wishlist item found for the given UserId.', 16, 1);
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

exec View_Wishlist_ByUserId_SP
		@UserId=1

select * from Users
select * from Books
select * from Wishlist