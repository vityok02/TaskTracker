CREATE TABLE Comment (
	Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
	Comment NVARCHAR(255) NOT NULL,
	CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
	CreatedBy UNIQUEIDENTIFIER NOT NULL,
	UpdatedAt DATETIME NULL,
	UpdatedBy UNIQUEIDENTIFIER NULL,
	UserId UNIQUEIDENTIFIER NOT NULL,
	TaskId UNIQUEIDENTIFIER NOT NULL,
	FOREIGN KEY (UserId) REFERENCES [User](Id),
	FOREIGN KEY (TaskId) REFERENCES Task(Id),
	FOREIGN KEY (CreatedBy) REFERENCES [User](Id),
	FOREIGN KEY (UpdatedBy) REFERENCES [User](Id)
)