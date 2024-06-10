use BookStore

--1)fetch book using book name and author name
create or alter procedure Get_Book_Byname_SP
(
	@BookName varchar(50),
    @AuthorName varchar(50)
)
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the Book exists
		if exists (select 1 from Books where Title = @BookName and Author = @AuthorName)
			begin
				select * from Books where Title = @BookName and Author = @AuthorName
			end
		else
			begin 
				RAISERROR('No book found for the given BookName and AuthorName.', 16, 1);
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

exec Get_Book_Byname_SP 
		@BookName = 'To Kill a Mockingbird',
		@AuthorName = 'Harper Lee'

--2) check how many books is there in a cart for a specific user and 
--print it alongwith user details(name and number + cart info)
create or alter procedure CountOfBooks_InCart_ByUserId_SP
@UserId int
as
begin
	begin try
		declare @UserName varchar(50),
				@PhoneNo varchar(50),
				@BookCount int

		-- Check if the User exists
		if exists (select 1 from Users where UserId=@UserId)
		begin
			 -- Get user details
			select @UserName = FullName,
				   @PhoneNo = MobileNo
			from Users
			where UserId = @UserId

			 -- Count the number of books in the cart for the specified user
			select @BookCount = COUNT(*)
			from Cart
			where UserId = @UserId

			 -- Retrieve user details and book count
			select @UserName AS UserName, 
				   @PhoneNo AS PhoneNo, 
				   @BookCount AS NumberOfBooksInCart

			-- Retrieve cart information
			select *
			from Cart
			where UserId = @UserId
		end
		else
			begin
				RAISERROR('User not found.', 16, 1);
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

exec CountOfBooks_InCart_ByUserId_SP
		@UserId=1

select * from Cart
select * from Books
select * from Users

