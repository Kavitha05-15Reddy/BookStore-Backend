use BookStore

--Create
create table Feedbacks
(
	FeedbackId int Identity(1,1) primary key,
	BookId int foreign key references Books(BookId),
	UserId int foreign key references Users(UserId),
	UserName varchar(50),
	Rating float,
	Review varchar(max)
)

select * from Feedbacks

--Add_Feedback
create or alter procedure Add_Feedback_SP
(
    @BookId int,
    @UserId int,
    @Rating float,
    @Review varchar(max)
)
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if any parameter is NULL or not valid
        if @BookId Is null or 
           @UserId Is null or 
           @Rating Is null or 
           @Review Is null or 
           @Rating < 0 or @Rating > 5
         begin
            RAISERROR('All parameters must be provided and valid.', 16, 1);
            return
        end

		declare @UserName varchar(50)

		-- Get the username from the Users table
		select @UserName = FullName from Users where UserId = @UserId

		-- Insert the feedback into the Feedbacks table
		insert into Feedbacks (BookId, UserId, UserName, Rating, Review)
		values (@BookId, @UserId, @UserName, @Rating, @Review)

		-- Check if the record was inserted successfully
		if @@ROWCOUNT = 1
        begin
            print 'Feedback inserted successfully.';
        end
        else
        begin
            RAISERROR('Error inserting Feedback record.', 16, 1);
            return
        end

		-- Update the rating and review count of the book
		update Books
		set Rating = (select AVG(Rating) from Feedbacks where BookId = @BookId),
			RatingCount = (select COUNT(*) from Feedbacks where BookId = @BookId)
		where BookId = @BookId;

		 -- Check if the update was successful
        if @@ROWCOUNT = 1
        begin
            print 'Book rating and review count updated successfully.';
        end
        ELSE
        begin
            RAISERROR('Error updating Book rating and review count.', 16, 1);
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

exec Add_Feedback_SP
		@BookId = 1,
		@UserId = 1,
		@Rating = 4,
		@Review = 'A powerful and moving story that remains relevant today.'

--GetAll_Feedbacks
create or alter procedure GetAll_Feedbacks_SP
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the Feedback exists
		if exists (select 1 from Feedbacks)
			begin
				select * from Feedbacks
			end
		else
			begin 
				RAISERROR('No Feedback found.', 16, 1);
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

exec GetAll_Feedbacks_SP 

--Update_Feedback
create or alter procedure Update_Feedback_SP
(
    @FeedbackId int,
    @Rating float,
    @Review varchar(max)
)
as
begin
	SET NOCOUNT ON;
	begin try
		declare @BookId int;

		-- Check if the FeedbackId exists
		if exists (select 1 from Feedbacks where FeedbackId=@FeedbackId)
		begin
			-- Get the BookId associated with the feedback
			select @BookId = BookId from Feedbacks where FeedbackId = @FeedbackId

			-- Update the feedback in the Feedbacks table
			update Feedbacks
			set Rating = @Rating,
				Review = @Review
			where FeedbackId = @FeedbackId

			-- Update the rating and review count of the book
			update Books
			set Rating = (select AVG(Rating) from Feedbacks where BookId = @BookId),
				RatingCount = (select COUNT(*) from Feedbacks where BookId = @BookId)
			where BookId = @BookId

			-- Check if any rows were affected by the update
			if @@ROWCOUNT = 1
			begin
				print 'Feedback updated successfully.'
			end
			else
			begin
				RAISERROR('Error updating Feedback.', 16, 1);
				return
			end
		end
		else
			begin
				RAISERROR('Feedback with provided FeedbackId does not exist.', 16, 1);
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

exec Update_Feedback_SP
		@FeedbackId = 5,
		@Rating = 3,
		@Review = 'An immersive and captivating journey through a richly detailed world.'

--Delete_Feedback
create or alter procedure Delete_Feedback_SP
@FeedbackId int
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the Feedback exists
		if exists (select 1 from Feedbacks where FeedbackId=@FeedbackId)
		begin
			 -- Delete the Feedback
			 delete from Feedbacks where FeedbackId = @FeedbackId

			  -- Check if any rows were affected by the delete operation
			  if @@ROWCOUNT = 1
			  begin
				 print 'Feedback deleted successfully.'
			  end
			  else
			  begin
				RAISERROR('Feedback could not be deleted.', 16, 1);
				return
			  end
		end
		else
			begin
				RAISERROR('Feedback not found.', 16, 1);
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

exec Delete_Feedback_SP 
		@FeedbackId=1

--GetAll_Feedbacks_ByBookId
create or alter procedure GetAll_Feedbacks_ByBookId_SP
@BookId int
as
begin
	SET NOCOUNT ON;
	begin try
		-- Check if the Feedback exists
		if exists (select 1 from Feedbacks where BookId=@BookId)
			begin
				select * from Feedbacks where BookId = @BookId
			end
		else
			begin 
				RAISERROR('No feedback found for the given BookId.', 16, 1);
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

exec GetAll_Feedbacks_ByBookId_SP
		@BookId=1


select * from Users
select * from Books
select * from Feedbacks