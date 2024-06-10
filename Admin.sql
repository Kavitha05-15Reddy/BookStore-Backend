use BookStore

--Create
create table Admin
(
	AdminId int Identity(1,1) primary key,
	FullName varchar(50),
	EmailId varchar(50),
	Password varchar(20),
	MobileNo varchar(15)
)

insert into Admin (FullName, EmailId, Password, MobileNo)
values ('Kavitha Reddy', 'kavithareddy3379@gmail.com', 'Krishna@123', '7995640903');

select * from Admin

--Admin_Login
create or alter procedure Admin_Login_SP
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

		-- Check if the Admin exists
		if exists (select 1 from Admin where EmailId=@EmailId and Password=@Password)
			begin
				select * from Admin where EmailId=@EmailId and Password=@Password
			end
		else
			begin
				RAISERROR('Invalid credentials. Login failed.', 16, 1);
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

exec Admin_Login_SP 
		@EmailId='kavithareddy3379@gmail.com',
		@Password='Krishna@123'

