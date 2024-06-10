use BookStore

--Create
create table Cart
(
	CartId int Identity(1,1) primary key,
	UserId int foreign key references Users(UserId),
	BookId int foreign key references Books(BookId),
	Title varchar(100),
	Author varchar(100),
	Image varchar(max),
	Quantity int,
	TotalPrice decimal(10, 2),
	TotalOriginalPrice decimal(10, 2)
)

select * from Cart

--AddBook_ToCart
create or alter procedure AddBook_ToCart_SP
(
    @UserId int,
    @BookId int,
	@Quantity int = 1

)
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
				@Price decimal(6, 2),
				@OriginalPrice decimal(6, 2);

		-- Get the details from the Book
		select @Title = Title,
			   @Author = Author,
			   @Image = Image,
			   @Price = Price, 
			   @OriginalPrice = OriginalPrice
		from Books
		where BookId = @BookId;

		-- Check if the book is already in the cart
        if not exists (select 1 from Cart where UserId = @UserId and BookId = @BookId)
        begin
            -- If book does not exist in the cart, insert new record
            insert into Cart (UserId, BookId, Title, Author, Image, Quantity,TotalPrice, TotalOriginalPrice)
            values (@UserId, @BookId,  @Title, @Author, @Image, @Quantity, @Quantity * @Price, @Quantity * @OriginalPrice);

			print 'Book added to cart successfully.';
        end
        else
        begin
            RAISERROR('The book is already in the cart.', 16, 1);
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

exec AddBook_ToCart_SP 
		@UserId = 1,
		@BookId = 6

--ViewCart
create or alter procedure ViewCart_SP
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the Cart exists
		if exists (select 1 from Cart)
			begin
				select * from Cart
			end
		else
			begin 
				RAISERROR('Empty Cart.', 16, 1);
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

exec ViewCart_SP 

--Update_Cart
create or alter procedure Update_Cart_SP
(
	@CartId int,
	@Quantity int
)
as 
begin
	SET NOCOUNT ON;
	begin try
		declare @Price decimal(6, 2);
		declare @OriginalPrice decimal(6, 2);
		declare @ExistingQuantity int;

		-- Check if the CartId exists
		if exists (select 1 from Cart where CartId=@CartId)
		begin
			-- Get book prices and existing quantity
			select @Price = b.Price, 
				   @OriginalPrice = b.OriginalPrice,
				   @ExistingQuantity = c.Quantity
			from Cart c, Books b 
			where c.BookId = b.BookId and 
				  c.CartId = @CartId;

			-- Update the quantity and prices
			update Cart
			set Quantity = @ExistingQuantity + @Quantity,
				TotalPrice = (@ExistingQuantity + @Quantity) * @Price,
				TotalOriginalPrice = (@ExistingQuantity + @Quantity) * @OriginalPrice
			where CartId = @CartId;

			-- Check if any rows were affected by the update
			if @@ROWCOUNT = 1
			begin
				print 'Cart item updated successfully.'
			end
			else
			begin
				RAISERROR('Error updating Cart item .', 16, 1);
				return
			end
		end
		else
			begin
				RAISERROR('Cart item with provided CartId does not exist.', 16, 1);
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

exec Update_Cart_SP 
		@CartId = 5,
		@Quantity = 1

--RemoveBook_FromCart
create or alter procedure RemoveBook_FromCart_SP
@CartId int
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the Cart item exists
		if exists (select 1 from Cart where CartId=@CartId)
		begin
			 -- Delete the Cart item
			 delete from Cart where CartId = @CartId

			  -- Check if any rows were affected by the delete operation
			  if @@ROWCOUNT = 1
			  begin
				 print 'Cart item deleted successfully.'
			  end
			  else
			  begin
				RAISERROR('Cart item could not be deleted.', 16, 1);
				return
			  end
		end
		else
			begin
				RAISERROR('Cart item not found.', 16, 1);
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

exec RemoveBook_FromCart_SP 
		@CartId=1

--ViewCart_ByUserId
create or alter procedure ViewCart_ByUserId_SP
@UserId int
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the Cart item exists
		if exists (select 1 from Cart where UserId = @UserId)
			begin
				select * from Cart where UserId = @UserId
			end
		else
			begin 
				RAISERROR('No Cart item found for the given UserId.', 16, 1);
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

exec ViewCart_ByUserId_SP 
		@UserId=1

select * from Cart
select * from Books
