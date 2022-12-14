USE [PEMS]
GO
/****** Object:  Table [dbo].[BeingLateTab]    Script Date: 02.08.2022 17:18:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BeingLateTab](
	[EmployeeID] [int] NULL,
	[BeingLateDate] [datetime2](7) NULL,
	[BeingLateTime] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 02.08.2022 17:18:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[ID] [int] NOT NULL,
	[Name] [nchar](10) NULL,
	[Surname] [nchar](10) NULL,
	[JobStartDate] [datetime2](7) NULL,
	[Address] [nchar](15) NULL,
	[WageRate] [real] NULL,
	[MonthlyWorkingTime] [int] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkingTimeTable]    Script Date: 02.08.2022 17:18:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkingTimeTable](
	[EmployeeID] [int] NULL,
	[Date] [date] NULL,
	[EntranceHour] [int] NULL,
	[EntranceMinute] [int] NULL,
	[ExitHour] [int] NULL,
	[ExitMinute] [nchar](10) NULL
) ON [PRIMARY]
GO
