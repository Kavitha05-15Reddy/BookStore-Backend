create database BookStore
use BookStore

--Create
create table Users
(
	UserId int Identity(1,1) primary key,
	FullName varchar(50) not null,
	EmailId varchar(50) not null,
	Password varchar(20) not null,
	MobileNo varchar(15) not null
)

select * from Users

--User_Register
create or alter procedure User_Register_SP
(
	@FullName varchar(50),
	@EmailId varchar(50),
	@Password varchar(20),
	@MobileNo varchar(15)
)
as 
begin
	SET NOCOUNT ON;
	begin try
		-- Check if any parameter is NULL
		if @FullName Is null or 
		@EmailId Is null or 
		@Password Is null or 
		@MobileNo Is null
		begin
			RAISERROR('All parameters must be provided.', 16, 1);
			return
		end

		-- Check if a user with the provided email already exists
		if exists (select 1 from Users where EmailId = @EmailId)
		begin
			RAISERROR('A user with this email already exists.', 16, 1);
			return
		end

		-- Check if Password meets length requirement
		if len(@Password) < 8
		begin
			RAISERROR('Password must be at least 8 characters long.', 16, 1);
			return
		end

		-- Check if MobileNo length is appropriate (example: 10 digits)
		if len(@MobileNo) <> 10
		begin
			RAISERROR('MobileNo must be exactly 10 digits long.', 16, 1);
			return
		end

		-- Insert the new User record
		Insert into Users (FullName,EmailId,Password,MobileNo)
		values (@FullName,@EmailId,@Password,@MobileNo)

		-- Check if the record was inserted successfully
		if @@Rowcount = 1
			begin
				print 'User inserted successfully.'
			end
		else
			begin
				RAISERROR('Error inserting User record.', 16, 1);
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

exec User_Register_SP 
		@FullName = 'pasala kavitha',
		@EmailId  =	'kavitha@gmail.com',
		@Password =	'kavitha@123',
		@MobileNo =	'7995540902'

--GetAll_Users
create or alter procedure GetAll_Users_SP
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the user exists
		if exists (select 1 from Users)
			begin
				select * from Users
			end
		else
			begin 
				RAISERROR('No Users found.', 16, 1);
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

exec GetAll_Users_SP

--Update_User
create or alter procedure Update_User_SP
(
	@UserId int,
	@FullName varchar(50),
	@EmailId varchar(50),
	@Password varchar(20),
	@MobileNo varchar(15)
)
as 
begin
	SET NOCOUNT ON;
	begin try
		-- Check if Password meets length requirement
		if len(@Password) < 8
		begin
			RAISERROR('Password must be at least 8 characters long.', 16, 1);
			return
		end

		-- Check if MobileNo length is appropriate (example: 10 digits)
		if len(@MobileNo) <> 10
		begin
			RAISERROR('MobileNo must be exactly 10 digits long.', 16, 1);
			return
		end

		-- Check if the User exists
		if exists (select 1 from Users where UserId=@UserId)
		begin
			 -- Update the User's information
			 update Users
			 set FullName=@FullName,
				 EmailId=@EmailId,
				 Password=@Password,
				 MobileNo=@MobileNo
			 where UserId=@UserId

			-- Check if any rows were affected by the update
			if @@ROWCOUNT = 1
			begin
				print 'User information updated successfully.'
			end
			else
			begin
				RAISERROR('User information could not be updated.', 16, 1);
				return
			end
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

exec Update_User_SP 
		@UserId = 1,
		@FullName = 'pasala kavitha',
		@EmailId  =	'kavitha@gmail.com',
		@Password =	'kavitha@123',
		@MobileNo =	'8995540902'

--Delete_User
create or alter procedure Delete_User_SP
@UserId int
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the User exists
		if exists (select 1 from Users where UserId=@UserId)
		begin
			 -- Delete the User
			 delete from Users where UserId=@UserId

			  -- Check if any rows were affected by the delete operation
			  if @@ROWCOUNT = 1
			  begin
				 print 'User deleted successfully.'
			  end
			  else
			  begin
				RAISERROR('User could not be deleted.', 16, 1);
				return
			  end
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

exec Delete_User_SP 
		@UserId=5

--User_Login
create or alter procedure User_Login_SP
(
	@EmailId varchar(50),
	@Password varchar(20)	
)
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if email and password are provided
		if @EmailId Is null or 
		@Password Is null
		begin
			RAISERROR('Email and Password must be provided.', 16, 1);
			return
		end

		-- Check if the User exists
		if exists (select 1 from Users where EmailId=@EmailId and Password=@Password)
			begin
				select * from Users where EmailId=@EmailId and Password=@Password
			end
		else
			begin
				RAISERROR('Invalid credentials. Login failed.', 16, 1);
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

exec User_Login_SP 
		@EmailId='kavitha@gmail.com',
		@Password='kavitha@123'

--Forgot_PassWord
create or alter procedure Forgot_PassWord_SP
@EmailId varchar(50)
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the User exists
		if exists (select 1 from Users where EmailId=@EmailId)
			begin
				select * from Users where EmailId=@EmailId
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

exec Forgot_PassWord_SP 
		@EmailId='kavitha@gmail.com'

--Reset_Password
create or alter procedure Reset_Password_SP
(
	@EmailId varchar(50),
	@NewPassword varchar(30)	
)
as
begin
	SET NOCOUNT ON;
	begin try
		-- Update the user's password
		update Users
		set Password =  @NewPassword
		where EmailId = @EmailId

		-- Check if the update was successful
		if @@ROWCOUNT = 1
			begin
				print 'Password Reset successfully.'
			end
		else
			begin
				RAISERROR('Password Reset Failed. Email not found.', 16, 1);
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

exec Reset_Password_SP 
		@EmailId='kavitha@gmail.com',
		@Password='kavitha@123'

select * from Users
