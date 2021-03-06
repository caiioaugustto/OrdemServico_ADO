USE [master]
GO
/****** Object:  Database [GerenciadorOrdemServico]    Script Date: 27/10/2016 23:58:34 ******/
CREATE DATABASE [GerenciadorOrdemServico]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GerenciadorOrdemServico', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\GerenciadorOrdemServico.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'GerenciadorOrdemServico_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\GerenciadorOrdemServico_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [GerenciadorOrdemServico] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GerenciadorOrdemServico].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GerenciadorOrdemServico] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET ARITHABORT OFF 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET  MULTI_USER 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GerenciadorOrdemServico] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [GerenciadorOrdemServico] SET DELAYED_DURABILITY = DISABLED 
GO
USE [GerenciadorOrdemServico]
GO
/****** Object:  Table [dbo].[Fornecedor]    Script Date: 27/10/2016 23:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Fornecedor](
	[Id] [int] NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[Telefone] [varchar](10) NULL,
	[NomeResponsavel] [nchar](25) NULL,
	[Email] [varchar](30) NULL,
	[Descricao] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Fornecedor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Login]    Script Date: 27/10/2016 23:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Login](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](10) NOT NULL,
	[Senha] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ordem]    Script Date: 27/10/2016 23:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ordem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DataSolicitacao] [datetime] NOT NULL,
	[NumeroOrdemServico] [varchar](20) NOT NULL,
	[NumeroCondominio] [int] NOT NULL,
	[Solicitante] [nchar](30) NULL,
	[Gerente] [nchar](30) NOT NULL,
	[Nucleo] [varchar](10) NOT NULL,
	[DataEnvio] [datetime] NOT NULL,
	[Prazo] [nchar](10) NOT NULL,
	[DataLiberacao] [datetime] NOT NULL,
	[Status] [varchar](10) NOT NULL,
	[DescricaoServico] [varchar](50) NOT NULL,
	[IdFornecedor] [int] NOT NULL,
 CONSTRAINT [PK_Ordem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Ordem]  WITH CHECK ADD  CONSTRAINT [FK_Ordem_Fornecedor] FOREIGN KEY([IdFornecedor])
REFERENCES [dbo].[Fornecedor] ([Id])
GO
ALTER TABLE [dbo].[Ordem] CHECK CONSTRAINT [FK_Ordem_Fornecedor]
GO
USE [master]
GO
ALTER DATABASE [GerenciadorOrdemServico] SET  READ_WRITE 
GO
