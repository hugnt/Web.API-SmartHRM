USE [master]
GO
/****** Object:  Database [SMARTHRM]    Script Date: 11/5/2023 3:59:27 AM ******/
CREATE DATABASE [SMARTHRM]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SMARTHRM', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\SMARTHRM.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SMARTHRM_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\SMARTHRM_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SMARTHRM] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SMARTHRM].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SMARTHRM] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SMARTHRM] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SMARTHRM] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SMARTHRM] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SMARTHRM] SET ARITHABORT OFF 
GO
ALTER DATABASE [SMARTHRM] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [SMARTHRM] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SMARTHRM] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SMARTHRM] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SMARTHRM] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SMARTHRM] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SMARTHRM] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SMARTHRM] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SMARTHRM] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SMARTHRM] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SMARTHRM] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SMARTHRM] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SMARTHRM] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SMARTHRM] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SMARTHRM] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SMARTHRM] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SMARTHRM] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SMARTHRM] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SMARTHRM] SET  MULTI_USER 
GO
ALTER DATABASE [SMARTHRM] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SMARTHRM] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SMARTHRM] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SMARTHRM] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SMARTHRM] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SMARTHRM] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [SMARTHRM] SET QUERY_STORE = OFF
GO
USE [SMARTHRM]
GO
/****** Object:  Table [dbo].[tAccount]    Script Date: 11/5/2023 3:59:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tAccount](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[role_id] [int] NULL,
	[username] [varchar](30) NULL,
	[password] [varchar](32) NULL,
	[employee_id] [int] NULL,
	[createdAt] [datetime] NULL,
	[updatedAt] [datetime] NULL,
	[createdBy] [int] NULL,
	[updatedBy] [int] NULL,
	[isDeleted] [bit] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [pk_tAccount] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tAllowance]    Script Date: 11/5/2023 3:59:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tAllowance](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[amount] [money] NULL,
	[expriredAt] [datetime] NULL,
	[note] [nvarchar](255) NULL,
	[createdAt] [datetime] NULL,
	[updatedAt] [datetime] NULL,
	[createdBy] [int] NULL,
	[updatedBy] [int] NULL,
	[isDeleted] [bit] NULL,
	[isActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tAllowanceDetails]    Script Date: 11/5/2023 3:59:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tAllowanceDetails](
	[allowance_id] [int] NOT NULL,
	[employee_id] [int] NOT NULL,
	[startAt] [datetime] NULL,
	[expriredAt] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tBonus]    Script Date: 11/5/2023 3:59:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tBonus](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[amount] [money] NULL,
	[expriredAt] [datetime] NULL,
	[note] [nvarchar](255) NULL,
	[createdAt] [datetime] NULL,
	[updatedAt] [datetime] NULL,
	[createdBy] [int] NULL,
	[updatedBy] [int] NULL,
	[isDeleted] [bit] NULL,
	[isActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tBonusDetails]    Script Date: 11/5/2023 3:59:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tBonusDetails](
	[bonus_id] [int] NOT NULL,
	[employee_id] [int] NOT NULL,
	[startAt] [datetime] NULL,
	[expriredAt] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tContract]    Script Date: 11/5/2023 3:59:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tContract](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[employee_id] [int] NULL,
	[signedAt] [datetime] NULL,
	[expriedAt] [datetime] NULL,
	[content] [nvarchar](1000) NULL,
	[image] [nvarchar](1000) NULL,
	[createdAt] [datetime] NULL,
	[updatedAt] [datetime] NULL,
	[createdBy] [int] NULL,
	[updatedBy] [int] NULL,
	[isDeleted] [bit] NULL,
	[isActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tDeduction]    Script Date: 11/5/2023 3:59:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tDeduction](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[amount] [money] NULL,
	[expriredAt] [datetime] NULL,
	[note] [nvarchar](255) NULL,
	[createdAt] [datetime] NULL,
	[updatedAt] [datetime] NULL,
	[createdBy] [int] NULL,
	[updatedBy] [int] NULL,
	[isDeleted] [bit] NULL,
	[isActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tDeductionDetails]    Script Date: 11/5/2023 3:59:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tDeductionDetails](
	[deduction_id] [int] NOT NULL,
	[employee_id] [int] NOT NULL,
	[startAt] [datetime] NULL,
	[expriredAt] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tDepartment]    Script Date: 11/5/2023 3:59:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tDepartment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[manager_id] [int] NULL,
	[description] [nvarchar](1000) NULL,
	[createdAt] [datetime] NULL,
	[updatedAt] [datetime] NULL,
	[createdBy] [int] NULL,
	[updatedBy] [int] NULL,
	[isDeleted] [bit] NULL,
	[isActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tEmployee]    Script Date: 11/5/2023 3:59:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tEmployee](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fullName] [nvarchar](30) NULL,
	[phoneNumber] [varchar](30) NULL,
	[email] [varchar](50) NULL,
	[dob] [datetime] NULL,
	[address] [nvarchar](255) NULL,
	[country] [nvarchar](255) NULL,
	[avatar] [varchar](100) NULL,
	[gender] [bit] NULL,
	[department_id] [int] NULL,
	[level] [nvarchar](30) NULL,
	[identificationCard] [varchar](100) NULL,
	[createdAt] [datetime] NULL,
	[updatedAt] [datetime] NULL,
	[createdBy] [int] NULL,
	[updatedBy] [int] NULL,
	[isDeleted] [bit] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK__tEmploye__3213E83F82501D90] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tInsurance]    Script Date: 11/5/2023 3:59:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tInsurance](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[benefits] [nvarchar](225) NULL,
	[requirement] [nvarchar](225) NULL,
	[amount] [money] NULL,
	[createdAt] [datetime] NULL,
	[updatedAt] [datetime] NULL,
	[createdBy] [int] NULL,
	[updatedBy] [int] NULL,
	[isDeleted] [bit] NULL,
	[isActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tInsuranceDetails]    Script Date: 11/5/2023 3:59:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tInsuranceDetails](
	[insurance_id] [int] NULL,
	[employee_id] [int] NULL,
	[provideAt] [datetime] NULL,
	[expriredAt] [datetime] NULL,
	[providerAddress] [nvarchar](50) NULL,
	[createdAt] [datetime] NULL,
	[updatedAt] [datetime] NULL,
	[createdBy] [int] NULL,
	[updatedBy] [int] NULL,
	[isDeleted] [bit] NULL,
	[isActive] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tPosition]    Script Date: 11/5/2023 3:59:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tPosition](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[createdAt] [datetime] NULL,
	[updatedAt] [datetime] NULL,
	[createdBy] [int] NULL,
	[updatedBy] [int] NULL,
	[isDeleted] [bit] NULL,
	[isActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tPositionDetails]    Script Date: 11/5/2023 3:59:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tPositionDetails](
	[employee_id] [int] NOT NULL,
	[position_id] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tProject]    Script Date: 11/5/2023 3:59:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tProject](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[leader_id] [int] NULL,
	[startedAt] [datetime] NULL,
	[endedAt] [datetime] NULL,
	[status] [int] NULL,
	[createdAt] [datetime] NULL,
	[updatedAt] [datetime] NULL,
	[createdBy] [int] NULL,
	[updatedBy] [int] NULL,
	[isDeleted] [bit] NULL,
	[isActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tProjectDetails]    Script Date: 11/5/2023 3:59:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tProjectDetails](
	[employee_id] [int] NOT NULL,
	[project_id] [int] NOT NULL,
	[joinedAt] [datetime] NULL,
	[leavedAt] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tRole]    Script Date: 11/5/2023 3:59:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tRole](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](20) NULL,
	[createdAt] [datetime] NULL,
	[updatedAt] [datetime] NULL,
 CONSTRAINT [pk_tRole] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tTask]    Script Date: 11/5/2023 3:59:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tTask](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[startTime] [datetime] NULL,
	[status] [int] NULL,
	[content] [nvarchar](1000) NULL,
	[description] [nvarchar](100) NULL,
	[createdAt] [datetime] NULL,
	[updatedAt] [datetime] NULL,
	[createdBy] [int] NULL,
	[updatedBy] [int] NULL,
	[isDeleted] [bit] NULL,
	[isActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tTaskDetails]    Script Date: 11/5/2023 3:59:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tTaskDetails](
	[assignee_id] [int] NOT NULL,
	[assigner_id] [int] NOT NULL,
	[task_id] [int] NOT NULL,
	[content] [nvarchar](1000) NULL,
	[description] [nvarchar](100) NULL,
	[status] [int] NULL,
	[createdAt] [datetime] NULL,
	[updatedAt] [datetime] NULL,
	[createdBy] [int] NULL,
	[updatedBy] [int] NULL,
	[isDeleted] [bit] NULL,
	[isActive] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tTimeKeeping]    Script Date: 11/5/2023 3:59:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tTimeKeeping](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[employee_id] [int] NULL,
	[timeAttendance] [datetime] NULL,
	[note] [nvarchar](255) NULL,
	[createdAt] [datetime] NULL,
	[updatedAt] [datetime] NULL,
	[createdBy] [int] NULL,
	[updatedBy] [int] NULL,
	[isDeleted] [bit] NULL,
	[isActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tEmployee] ON 

INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (3, N'Nguyễn Thành Hưng', N'0946928815', N'thanh.hung.st302@gmail.com', CAST(N'2003-02-13T00:00:00.000' AS DateTime), N'Thanh Xuân, Hà Nội', NULL, NULL, 1, NULL, N'Senior', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (4, N'Nguyễn Đình Lê Hoàng', N'0921312322', N'hoanghn@gmail.com', CAST(N'2003-10-12T00:00:00.000' AS DateTime), N'Cầu giấy, Hà Nội', NULL, NULL, 1, NULL, N'Senior', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (5, N'Ngô Việt Đức', N'0946923815', N'vietducnguyen@gmail.com', CAST(N'2003-10-05T00:00:00.000' AS DateTime), N'Khương đình, Hà Nội', NULL, NULL, 1, NULL, N'Senior', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (6, N'Vũ Văn Sử', N'0941228815', N'sulv@gmail.com', CAST(N'2003-01-01T00:00:00.000' AS DateTime), N'Bắc Ninh', NULL, NULL, 1, NULL, N'Senior', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (26, N'Hoàng Hồ Gia Khánh', N'0946123815', N'giakhanh@gmail.com', CAST(N'2003-02-09T00:00:00.000' AS DateTime), N'Thanh Hóa', NULL, NULL, 1, NULL, N'Senior', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (45, N'Nguyễn Văn A', N'0123456789', N'nguyenvana@example.com', CAST(N'1990-01-01T00:00:00.000' AS DateTime), N'Địa chỉ A', NULL, NULL, 1, NULL, N'Level 1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (46, N'Trần Thị B', N'0987654321', N'tranthib@example.com', CAST(N'1995-02-02T00:00:00.000' AS DateTime), N'Địa chỉ B', NULL, NULL, 0, NULL, N'Level 2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (47, N'Lê Hoàng C', N'0123456789', N'lehoangc@example.com', CAST(N'1988-03-03T00:00:00.000' AS DateTime), N'Địa chỉ C', NULL, NULL, 1, NULL, N'Level 1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (48, N'Phạm Thị D', N'0987654321', N'phamthid@example.com', CAST(N'1992-04-04T00:00:00.000' AS DateTime), N'Địa chỉ D', NULL, NULL, 0, NULL, N'Level 2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (49, N'Nguyễn Văn E', N'0123456789', N'nguyenvane@example.com', CAST(N'1991-05-05T00:00:00.000' AS DateTime), N'Địa chỉ E', NULL, NULL, 1, NULL, N'Level 1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (50, N'Trần Thị F', N'0987654321', N'tranthif@example.com', CAST(N'1994-06-06T00:00:00.000' AS DateTime), N'Địa chỉ F', NULL, NULL, 0, NULL, N'Level 2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (51, N'Lê Hoàng G', N'0123456789', N'lehoangg@example.com', CAST(N'1989-07-07T00:00:00.000' AS DateTime), N'Địa chỉ G', NULL, NULL, 1, NULL, N'Level 1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (52, N'Phạm Thị H', N'0987654321', N'phamthih@example.com', CAST(N'1993-08-08T00:00:00.000' AS DateTime), N'Địa chỉ H', NULL, NULL, 0, NULL, N'Level 2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (53, N'Nguyễn Văn I', N'0123456789', N'nguyenvani@example.com', CAST(N'1990-09-09T00:00:00.000' AS DateTime), N'Địa chỉ I', NULL, NULL, 1, NULL, N'Level 1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (54, N'Trần Thị J', N'0987654321', N'tranthij@example.com', CAST(N'1995-10-10T00:00:00.000' AS DateTime), N'Địa chỉ J', NULL, NULL, 0, NULL, N'Level 2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (55, N'Lê Hoàng K', N'0123456789', N'lehoangk@example.com', CAST(N'1988-11-11T00:00:00.000' AS DateTime), N'Địa chỉ K', NULL, NULL, 1, NULL, N'Level 1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (56, N'Phạm Thị L', N'0987654321', N'phamthil@example.com', CAST(N'1994-12-12T00:00:00.000' AS DateTime), N'Địa chỉ L', NULL, NULL, 0, NULL, N'Level 2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (57, N'Nguyễn Văn M', N'0123456789', N'nguyenvanm@example.com', CAST(N'1993-01-01T00:00:00.000' AS DateTime), N'Địa chỉ M', NULL, NULL, 1, NULL, N'Level 1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (58, N'Trần Thị N', N'0987654321', N'tranthin@example.com', CAST(N'1997-02-02T00:00:00.000' AS DateTime), N'Địa chỉ N', NULL, NULL, 0, NULL, N'Level 2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (59, N'Lê Hoàng O', N'0123456789', N'lehoango@example.com', CAST(N'1985-03-03T00:00:00.000' AS DateTime), N'Địa chỉ O', NULL, NULL, 1, NULL, N'Level 1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (60, N'Phạm Thị P', N'0987654321', N'phamthip@example.com', CAST(N'1990-04-04T00:00:00.000' AS DateTime), N'Địa chỉ P', NULL, NULL, 0, NULL, N'Level 2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (61, N'Nguyễn Văn Q', N'0123456789', N'nguyenvanq@example.com', CAST(N'1989-05-05T00:00:00.000' AS DateTime), N'Địa chỉ Q', NULL, NULL, 1, NULL, N'Level 1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tEmployee] ([id], [fullName], [phoneNumber], [email], [dob], [address], [country], [avatar], [gender], [department_id], [level], [identificationCard], [createdAt], [updatedAt], [createdBy], [updatedBy], [isDeleted], [isActive]) VALUES (62, N'Trần Thị R', N'0987654321', N'tranthir@example.com', CAST(N'1992-06-06T00:00:00.000' AS DateTime), N'Địa chỉ R', NULL, NULL, 0, NULL, N'Level 2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[tEmployee] OFF
GO
ALTER TABLE [dbo].[tAccount]  WITH CHECK ADD  CONSTRAINT [fk_tRole_tAccount_id] FOREIGN KEY([role_id])
REFERENCES [dbo].[tRole] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tAccount] CHECK CONSTRAINT [fk_tRole_tAccount_id]
GO
ALTER TABLE [dbo].[tAllowanceDetails]  WITH CHECK ADD  CONSTRAINT [FK__tAllowanc__emplo__35BCFE0A] FOREIGN KEY([employee_id])
REFERENCES [dbo].[tEmployee] ([id])
GO
ALTER TABLE [dbo].[tAllowanceDetails] CHECK CONSTRAINT [FK__tAllowanc__emplo__35BCFE0A]
GO
ALTER TABLE [dbo].[tAllowanceDetails]  WITH CHECK ADD  CONSTRAINT [FK_tAllowanceDetails_tAllowance] FOREIGN KEY([allowance_id])
REFERENCES [dbo].[tAllowance] ([id])
GO
ALTER TABLE [dbo].[tAllowanceDetails] CHECK CONSTRAINT [FK_tAllowanceDetails_tAllowance]
GO
ALTER TABLE [dbo].[tBonusDetails]  WITH CHECK ADD  CONSTRAINT [FK__tBonusDet__emplo__3A81B327] FOREIGN KEY([employee_id])
REFERENCES [dbo].[tEmployee] ([id])
GO
ALTER TABLE [dbo].[tBonusDetails] CHECK CONSTRAINT [FK__tBonusDet__emplo__3A81B327]
GO
ALTER TABLE [dbo].[tBonusDetails]  WITH CHECK ADD  CONSTRAINT [FK_tBonusDetails_tBonus] FOREIGN KEY([bonus_id])
REFERENCES [dbo].[tBonus] ([id])
GO
ALTER TABLE [dbo].[tBonusDetails] CHECK CONSTRAINT [FK_tBonusDetails_tBonus]
GO
ALTER TABLE [dbo].[tContract]  WITH CHECK ADD  CONSTRAINT [FK__tContract__emplo__628FA481] FOREIGN KEY([employee_id])
REFERENCES [dbo].[tEmployee] ([id])
GO
ALTER TABLE [dbo].[tContract] CHECK CONSTRAINT [FK__tContract__emplo__628FA481]
GO
ALTER TABLE [dbo].[tDeductionDetails]  WITH CHECK ADD  CONSTRAINT [FK__tDeductio__emplo__3F466844] FOREIGN KEY([employee_id])
REFERENCES [dbo].[tEmployee] ([id])
GO
ALTER TABLE [dbo].[tDeductionDetails] CHECK CONSTRAINT [FK__tDeductio__emplo__3F466844]
GO
ALTER TABLE [dbo].[tDeductionDetails]  WITH CHECK ADD  CONSTRAINT [FK_tDeductionDetails_tDeduction] FOREIGN KEY([deduction_id])
REFERENCES [dbo].[tDeduction] ([id])
GO
ALTER TABLE [dbo].[tDeductionDetails] CHECK CONSTRAINT [FK_tDeductionDetails_tDeduction]
GO
ALTER TABLE [dbo].[tDepartment]  WITH CHECK ADD  CONSTRAINT [FK__tDepartme__manag__5FB337D6] FOREIGN KEY([manager_id])
REFERENCES [dbo].[tEmployee] ([id])
GO
ALTER TABLE [dbo].[tDepartment] CHECK CONSTRAINT [FK__tDepartme__manag__5FB337D6]
GO
ALTER TABLE [dbo].[tEmployee]  WITH CHECK ADD  CONSTRAINT [FK_tEmployee_tDepartment] FOREIGN KEY([department_id])
REFERENCES [dbo].[tDepartment] ([id])
GO
ALTER TABLE [dbo].[tEmployee] CHECK CONSTRAINT [FK_tEmployee_tDepartment]
GO
ALTER TABLE [dbo].[tInsuranceDetails]  WITH CHECK ADD  CONSTRAINT [FK__tInsuranc__emplo__534D60F1] FOREIGN KEY([employee_id])
REFERENCES [dbo].[tEmployee] ([id])
GO
ALTER TABLE [dbo].[tInsuranceDetails] CHECK CONSTRAINT [FK__tInsuranc__emplo__534D60F1]
GO
ALTER TABLE [dbo].[tInsuranceDetails]  WITH CHECK ADD FOREIGN KEY([insurance_id])
REFERENCES [dbo].[tInsurance] ([id])
GO
ALTER TABLE [dbo].[tPositionDetails]  WITH CHECK ADD  CONSTRAINT [FK__tPosition__emplo__49C3F6B7] FOREIGN KEY([employee_id])
REFERENCES [dbo].[tEmployee] ([id])
GO
ALTER TABLE [dbo].[tPositionDetails] CHECK CONSTRAINT [FK__tPosition__emplo__49C3F6B7]
GO
ALTER TABLE [dbo].[tPositionDetails]  WITH CHECK ADD FOREIGN KEY([position_id])
REFERENCES [dbo].[tPosition] ([id])
GO
ALTER TABLE [dbo].[tProject]  WITH CHECK ADD  CONSTRAINT [FK__tProject__leader__5629CD9C] FOREIGN KEY([leader_id])
REFERENCES [dbo].[tEmployee] ([id])
GO
ALTER TABLE [dbo].[tProject] CHECK CONSTRAINT [FK__tProject__leader__5629CD9C]
GO
ALTER TABLE [dbo].[tProjectDetails]  WITH CHECK ADD  CONSTRAINT [FK__tProjectD__emplo__5BE2A6F2] FOREIGN KEY([employee_id])
REFERENCES [dbo].[tEmployee] ([id])
GO
ALTER TABLE [dbo].[tProjectDetails] CHECK CONSTRAINT [FK__tProjectD__emplo__5BE2A6F2]
GO
ALTER TABLE [dbo].[tProjectDetails]  WITH CHECK ADD FOREIGN KEY([project_id])
REFERENCES [dbo].[tProject] ([id])
GO
ALTER TABLE [dbo].[tTaskDetails]  WITH CHECK ADD  CONSTRAINT [FK__tTaskDeta__assig__440B1D61] FOREIGN KEY([assignee_id])
REFERENCES [dbo].[tEmployee] ([id])
GO
ALTER TABLE [dbo].[tTaskDetails] CHECK CONSTRAINT [FK__tTaskDeta__assig__440B1D61]
GO
ALTER TABLE [dbo].[tTaskDetails]  WITH CHECK ADD  CONSTRAINT [FK__tTaskDeta__assig__44FF419A] FOREIGN KEY([assigner_id])
REFERENCES [dbo].[tEmployee] ([id])
GO
ALTER TABLE [dbo].[tTaskDetails] CHECK CONSTRAINT [FK__tTaskDeta__assig__44FF419A]
GO
ALTER TABLE [dbo].[tTaskDetails]  WITH CHECK ADD FOREIGN KEY([task_id])
REFERENCES [dbo].[tTask] ([id])
GO
ALTER TABLE [dbo].[tTimeKeeping]  WITH CHECK ADD  CONSTRAINT [FK__tTimeKeep__emplo__30F848ED] FOREIGN KEY([employee_id])
REFERENCES [dbo].[tEmployee] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tTimeKeeping] CHECK CONSTRAINT [FK__tTimeKeep__emplo__30F848ED]
GO
USE [master]
GO
ALTER DATABASE [SMARTHRM] SET  READ_WRITE 
GO
