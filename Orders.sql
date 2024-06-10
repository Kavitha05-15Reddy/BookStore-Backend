use BookStore

--Create
create table Orders
(
    OrderId int Identity(1,1) primary key,
	UserId int foreign key references Users(UserId),
	BookId int foreign key references Books(BookId),
	CartId int foreign key references Cart(CartId),
	Title varchar(100),
	Author varchar(100),
	Image varchar(max),
	Quantity int,
	TotalPrice decimal(10, 2),
	TotalOriginalPrice decimal(10, 2),
    OrderDateTime datetime default GETDATE()
)

select * from Orders

--PlaceOrder
create or alter procedure Place_Order_SP
@CartId int
as
begin
	SET NOCOUNT ON;
	begin try
		declare @UserId int, 
				@BookId int, 
				@Title varchar(50),
				@Author varchar(50),
				@Image varchar(max),
				@Quantity int, 
				@TotalPrice decimal(10, 2), 
				@TotalOriginalPrice decimal(10, 2)
	
		-- Get the details from the cart
		select @UserId = c.UserId, 
			   @BookId = c.BookId, 
			   @Title = b.Title,
			   @Author = b.Author,
			   @Image = b.Image,
			   @Quantity = c.Quantity, 
			   @TotalPrice = c.TotalPrice, 
			   @TotalOriginalPrice = c.TotalOriginalPrice
		from Cart c, Books b
		where c.BookId = b.BookId and
			  CartId = @CartId

		-- Check if the book is available in the required quantity
		if exists (select 1 from Books where BookId = @BookId and Quantity >= @Quantity)
		begin
			-- Insert into Orders
			insert into Orders (UserId, BookId, Title, Author, Image, Quantity, TotalPrice, TotalOriginalPrice)
			values (@UserId, @BookId, @Title, @Author, @Image, @Quantity, @TotalPrice, @TotalOriginalPrice);

			-- Decrease the quantity in Books table
			update Books
			set Quantity = Quantity - @Quantity
			where BookId = @BookId;

			-- Remove the item from the cart
			delete from Cart
			where CartId = @CartId;
		end
		else
		begin
			RAISERROR ('The book is not available in the cart or out of stock.', 16, 1);
			return;
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

exec Place_Order_SP 
		@CartId=4

--GetAll_Orders
create or alter procedure GetAll_Orders_SP
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the Order exists
		if exists (select 1 from Orders)
			begin
				select * from Orders
			end
		else
			begin 
				RAISERROR ('No Order found.', 16, 1);
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

exec GetAll_Orders_SP 
		
--Delete_Order
create or alter procedure Delete_Order_SP
@OrderId int
as
begin
	SET NOCOUNT ON;
	begin try
		declare @BookId int, 
				@Quantity int;

		 -- Get BookId and Quantity from the order
		select @BookId = BookId, @Quantity = Quantity
		from Orders
		where OrderId = @OrderId;

		-- Check if the order exists
		if exists (select 1 from Orders where OrderId = @OrderId)
		begin
			-- Delete the order
			delete from Orders where OrderId = @OrderId

			-- Restore the quantity of the book in the Books table
			update Books
			set Quantity = Quantity + @Quantity
			where BookId = @BookId;

			print 'Order deleted successfully and book restored to cart.'
		end
		else
		begin
			RAISERROR ('Order not found.', 16, 1);
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

exec Delete_Order_SP 
		@OrderId=4

--GetAllOrders_ByUserId
create or alter procedure GetAllOrders_ByUserId_SP
@UserId int
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the Order exists
		if exists (select * from Orders where UserId = @UserId)
			begin
				select * from Orders where UserId = @UserId
			end
		else
			begin 
				RAISERROR('No Order found for the given UserId.', 16, 1);
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

exec GetAllOrders_ByUserId_SP 
		@UserId=1

select * from Books
select * from Cart
select * from Orders


