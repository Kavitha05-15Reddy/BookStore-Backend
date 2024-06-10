use BookStore

--create
create table Address 
(
    AddressId int Identity(1,1) primary key,
    UserId int foreign key references Users(UserId),
    FullName varchar(50),
    MobileNo varchar(15),
    Address varchar(max),
    City varchar(50),
    State varchar(50),
    Type varchar(10)
)

select * from Address

--Add_Address
create or alter procedure Add_Address_SP
(
    @UserId int,
    @FullName varchar(50),
    @MobileNo varchar(15),
    @Address varchar(max),
    @City varchar(50),
    @State varchar(50),
    @Type varchar(10)
)
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if any parameter is NULL or not valid
        if @UserId Is null or 
           @FullName Is null or 
           @MobileNo Is null or 
           @Address Is null or  
           @City Is null or 
           @State Is null or 
           @Type Is null or 
           LEN(@MobileNo) <> 10
         begin
            RAISERROR('All parameters must be provided and valid.', 16, 1);
            return
         end

		-- Insert the Address details into the Address table
		insert into Address (UserId, FullName, MobileNo, Address, City, State, Type)
		values (@UserId, @FullName, @MobileNo, @Address, @City, @State, @Type)

		-- Check if the record was inserted successfully
		if @@ROWCOUNT = 1
        begin
            print 'Address inserted successfully.';
        end
        else
        begin
            RAISERROR('Error inserting Address record.', 16, 1);
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

exec Add_Address_SP
		@UserId = 1,
		@FullName = 'Kavitha Reddy',
		@MobileNo = '7876543245',
		@Address = '456, MG Road, Near Bus Stand',
		@City = 'Vijayawada',
		@State = 'Andhra Pradesh',
		@Type = 'Home';

--GetAll_Addresses
create or alter procedure GetAll_Addresses_SP
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the Address exists
		if exists (select 1 from Address)
			begin
				select * from Address
			end
		else
			begin 
				RAISERROR('No Address found.', 16, 1);
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

exec GetAll_Addresses_SP 

--Update_Address
create or alter procedure Update_Address_SP
(
    @AddressId int,
    @FullName varchar(50),
    @MobileNo varchar(15),
    @Address varchar(max),
    @City varchar(50),
    @State varchar(50),
    @Type varchar(10)
)
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the AddressId exists
		if exists (select 1 from Address where AddressId=@AddressId)
		begin
			update Address
			set FullName = @FullName,
				MobileNo = @MobileNo,
				Address = @Address,
				City = @City,
				State = @State,
				Type = @Type
			where AddressId = @AddressId

			-- Check if any rows were affected by the update
			if @@ROWCOUNT = 1
			begin
				print 'Address updated successfully.'
			end
			else
			begin
				RAISERROR('Error updating Address.', 16, 1);
				return
			end
		end
		else
			begin
				RAISERROR('Address with provided AddressId does not exist.', 16, 1);
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

exec Update_Address_SP
		@AddressId = 1,
		@FullName = 'Pasala Kavitha',
		@MobileNo = '7989765435',
		@Address = '1/1A, Jubilee Hills, Near Film Nagar',
		@City = 'Banglore',
		@State = 'Karnataka',
		@Type = 'Work'

--Delete_Address
create or alter procedure Delete_Address_SP
@AddressId int
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the Address exists
		if exists (select 1 from Address where AddressId=@AddressId)
		begin
			 -- Delete the Address
			 delete from Address where AddressId = @AddressId

			  -- Check if any rows were affected by the delete operation
			  if @@ROWCOUNT = 1
			  begin
				 print 'Address deleted successfully.'
			  end
			  else
			  begin
				RAISERROR('Address could not be deleted.', 16, 1);
				return
			  end
		end
		else
			begin
				RAISERROR('Address not found.', 16, 1);
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

exec Delete_Address_SP 
		@AddressId=1

--GetAddress_ByUserId
create or alter procedure GetAddress_ByUserId_SP
@UserId int
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the Address exists
		if exists (select 1 from Address where UserId = @UserId)
			begin
				select * from Address where UserId = @UserId
			end
		else
			begin 
				RAISERROR('No address found for the given UserId.', 16, 1);
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

exec GetAddress_ByUserId_SP 
		@UserId=1

select * from Address