USE [master]
GO

CREATE DATABASE [sabado_db]
GO

USE [sabado_db]
GO

CREATE TABLE [dbo].[tCategoria](
	[IdCategoria] [smallint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tCategoria] PRIMARY KEY CLUSTERED 
(
	[IdCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tRol](
	[IdRol] [smallint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tRol] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tServicio](
	[IdServicio] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
	[Imagen] [varchar](500) NOT NULL,
	[Video] [varchar](500) NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_tServicio] PRIMARY KEY CLUSTERED 
(
	[IdServicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tUsuario](
	[IdUsuario] [bigint] IDENTITY(1,1) NOT NULL,
	[Correo] [varchar](200) NOT NULL,
	[Contrasenna] [varchar](200) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[IdRol] [smallint] NOT NULL,
	[Estado] [bit] NOT NULL,
	[EsTemporal] [bit] NOT NULL,
	[IdCategoria] [smallint] NOT NULL,
 CONSTRAINT [PK_tUsuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[tCategoria] ON 
GO
INSERT [dbo].[tCategoria] ([IdCategoria], [Nombre]) VALUES (1, N'Platino')
GO
INSERT [dbo].[tCategoria] ([IdCategoria], [Nombre]) VALUES (2, N'Diamante')
GO
INSERT [dbo].[tCategoria] ([IdCategoria], [Nombre]) VALUES (3, N'Oro')
GO
SET IDENTITY_INSERT [dbo].[tCategoria] OFF
GO

SET IDENTITY_INSERT [dbo].[tRol] ON 
GO
INSERT [dbo].[tRol] ([IdRol], [Nombre]) VALUES (1, N'Cliente')
GO
INSERT [dbo].[tRol] ([IdRol], [Nombre]) VALUES (2, N'Administrador')
GO
SET IDENTITY_INSERT [dbo].[tRol] OFF
GO

SET IDENTITY_INSERT [dbo].[tServicio] ON 
GO
INSERT [dbo].[tServicio] ([IdServicio], [Nombre], [Precio], [Imagen], [Video], [Estado]) VALUES (2, N'Odontología', CAST(25000.00 AS Decimal(18, 2)), N'PRUEBA', N'PRUEBA', 1)
GO
INSERT [dbo].[tServicio] ([IdServicio], [Nombre], [Precio], [Imagen], [Video], [Estado]) VALUES (3, N'Medicina General', CAST(20000.00 AS Decimal(18, 2)), N'PRUEBA', N'PRUEBA', 1)
GO
SET IDENTITY_INSERT [dbo].[tServicio] OFF
GO

SET IDENTITY_INSERT [dbo].[tUsuario] ON 
GO
INSERT [dbo].[tUsuario] ([IdUsuario], [Correo], [Contrasenna], [Nombre], [IdRol], [Estado], [EsTemporal], [IdCategoria]) VALUES (1, N'cvasquez10821@ufide.ac.cr', N'mzhhEyLuYke4Q8wSs8gHuA==', N'Claudio Vasquez', 1, 1, 0, 3)
GO
SET IDENTITY_INSERT [dbo].[tUsuario] OFF
GO

ALTER TABLE [dbo].[tUsuario] ADD  CONSTRAINT [UK_Correo] UNIQUE NONCLUSTERED 
(
	[Correo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tUsuario]  WITH CHECK ADD  CONSTRAINT [FK_tUsuario_tCategoria] FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[tCategoria] ([IdCategoria])
GO
ALTER TABLE [dbo].[tUsuario] CHECK CONSTRAINT [FK_tUsuario_tCategoria]
GO

ALTER TABLE [dbo].[tUsuario]  WITH CHECK ADD  CONSTRAINT [FK_tUsuario_tRol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[tRol] ([IdRol])
GO
ALTER TABLE [dbo].[tUsuario] CHECK CONSTRAINT [FK_tUsuario_tRol]
GO

CREATE PROCEDURE [dbo].[CambiarContrasenna]
	@Correo					VARCHAR(200),
	@Contrasenna			VARCHAR(200),
	@ContrasennaTemporal	VARCHAR(200),
	@EsTemporal				BIT
AS
BEGIN

	DECLARE @Consecutivo BIGINT
	
	SELECT  @Consecutivo = IdUsuario
	FROM	tUsuario
	WHERE	Correo = @Correo
		AND Contrasenna = @ContrasennaTemporal
		AND Estado = 1

	IF @Consecutivo IS NOT NULL
	BEGIN
		UPDATE tUsuario
		SET Contrasenna = @Contrasenna,
			EsTemporal = @EsTemporal
		WHERE Correo = @Correo
	END

	SELECT	IdUsuario,Correo,U.Nombre 'NombreUsuario',U.IdRol,R.Nombre 'NombreRol',Estado,EsTemporal
	  FROM	tUsuario U
	  INNER JOIN tRol R ON U.IdRol = R.IdRol
	  WHERE	Correo = @Correo
		AND Estado = 1

END
GO

CREATE PROCEDURE [dbo].[ConsultarServicios]
	@MostrarTodos BIT
AS
BEGIN

	IF(@MostrarTodos = 1)
	BEGIN
		SELECT	IdServicio,Nombre,Precio,Imagen,Video,Estado
		FROM	dbo.tServicio
	END
	ELSE
	BEGIN
		SELECT	IdServicio,Nombre,Precio,Imagen,Video,Estado
		FROM	dbo.tServicio
		WHERE	Estado = 1
	END
END
GO

CREATE PROCEDURE [dbo].[IniciarSesion]
	@Correo			VARCHAR(200),
    @Contrasenna	VARCHAR(200)
AS
BEGIN
	
	SELECT	IdUsuario,Correo,U.Nombre 'NombreUsuario',U.IdRol,R.Nombre 'NombreRol',Estado,EsTemporal,U.IdCategoria,C.Nombre 'NombreCategoria'
	  FROM	tUsuario U
	  INNER JOIN tRol R ON U.IdRol = R.IdRol
	  INNER JOIN tCategoria C ON u.IdCategoria = C.IdCategoria
	  WHERE	Correo = @Correo
		AND Contrasenna = @Contrasenna
		AND Estado = 1

END
GO

CREATE PROCEDURE [dbo].[RecuperarAcceso]
	@Correo			VARCHAR(200),
	@Contrasenna	VARCHAR(200),
	@EsTemporal		BIT
AS
BEGIN

	DECLARE @Consecutivo BIGINT
	
	SELECT  @Consecutivo = IdUsuario
	FROM	tUsuario
	WHERE	Correo = @Correo
		AND Estado = 1

	IF @Consecutivo IS NOT NULL
	BEGIN
		UPDATE tUsuario
		SET Contrasenna = @Contrasenna,
			EsTemporal = @EsTemporal
		WHERE Correo = @Correo
	END

	SELECT	IdUsuario,Correo,U.Nombre 'NombreUsuario',U.IdRol,R.Nombre 'NombreRol',Estado,EsTemporal
	  FROM	tUsuario U
	  INNER JOIN tRol R ON U.IdRol = R.IdRol
	  WHERE	Correo = @Correo
		AND Estado = 1

END
GO

CREATE PROCEDURE [dbo].[RegistrarServicio]
	@Nombre	varchar(200),
	@Precio decimal(18,2),
	@Imagen varchar(500),
	@Video varchar(500)
AS
BEGIN
	
	IF NOT EXISTS(SELECT 1 FROM tServicio WHERE Nombre = @Nombre)
	BEGIN

		INSERT INTO dbo.tServicio(Nombre,Precio,Imagen,Video,Estado)
		VALUES (@Nombre,@Precio,@Imagen,@Video,1)

	END

END
GO

CREATE PROCEDURE [dbo].[RegistrarUsuario]
	@Correo			VARCHAR(200),
    @Contrasenna	VARCHAR(200),
    @NombreUsuario	VARCHAR(200)
AS
BEGIN

	IF NOT EXISTS(SELECT 1 FROM tUsuario WHERE Correo = @Correo)
	BEGIN

		INSERT INTO dbo.tUsuario(Correo,Contrasenna,Nombre,IdRol,Estado,EsTemporal,IdCategoria)
		VALUES (@Correo,@Contrasenna,@NombreUsuario,1,1,0,3)

	END

END
GO