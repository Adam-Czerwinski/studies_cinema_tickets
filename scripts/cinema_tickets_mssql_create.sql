CREATE TABLE [employees] (
	id bigint IDENTITY(1,1),
	name varchar(50) NOT NULL,
	last_name varchar(50) NOT NULL,
	login varchar(18) NOT NULL UNIQUE,
	password varchar(255) NOT NULL,
  CONSTRAINT [PK_EMPLOYEES] PRIMARY KEY CLUSTERED
  (
  [id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [clients] (
	id bigint IDENTITY(1,1),
	login varchar(18) NOT NULL UNIQUE,
	password varchar(255) NOT NULL,
  CONSTRAINT [PK_CLIENTS] PRIMARY KEY CLUSTERED
  (
  [id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [movies] (
	id bigint IDENTITY(1,1),
	title varchar(255) NOT NULL,
	year date NOT NULL,
	duration int NOT NULL,
  CONSTRAINT [PK_MOVIES] PRIMARY KEY CLUSTERED
  (
  [id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [halls] (
	id bigint IDENTITY(1,1),
	room_number int NOT NULL,
	size int NOT NULL,
  CONSTRAINT [PK_HALLS] PRIMARY KEY CLUSTERED
  (
  [id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [movies_halls] (
	id_movie bigint NOT NULL,
	id_hall bigint NOT NULL,
	start_time datetime NOT NULL,
	end_time datetime NOT NULL
)
GO
CREATE TABLE [clients_movies_halls] (
	id_client bigint NOT NULL,
	id_movie bigint NOT NULL,
	id_hall bigint NOT NULL
)
GO




ALTER TABLE [movies_halls] WITH CHECK ADD CONSTRAINT [movies_halls_fk0] FOREIGN KEY ([id_movie]) REFERENCES [movies]([id])
ON UPDATE CASCADE
GO
ALTER TABLE [movies_halls] CHECK CONSTRAINT [movies_halls_fk0]
GO
ALTER TABLE [movies_halls] WITH CHECK ADD CONSTRAINT [movies_halls_fk1] FOREIGN KEY ([id_hall]) REFERENCES [halls]([id])
ON UPDATE CASCADE
GO
ALTER TABLE [movies_halls] CHECK CONSTRAINT [movies_halls_fk1]
GO

ALTER TABLE [clients_movies_halls] WITH CHECK ADD CONSTRAINT [clients_movies_halls_fk0] FOREIGN KEY ([id_client]) REFERENCES [clients]([id])
ON UPDATE CASCADE
GO
ALTER TABLE [clients_movies_halls] CHECK CONSTRAINT [clients_movies_halls_fk0]
GO
ALTER TABLE [clients_movies_halls] WITH CHECK ADD CONSTRAINT [clients_movies_halls_fk1] FOREIGN KEY ([id_movie]) REFERENCES [movies_halls]([id_movie])
ON UPDATE CASCADE
GO
ALTER TABLE [clients_movies_halls] CHECK CONSTRAINT [clients_movies_halls_fk1]
GO
ALTER TABLE [clients_movies_halls] WITH CHECK ADD CONSTRAINT [clients_movies_halls_fk2] FOREIGN KEY ([id_hall]) REFERENCES [movies_halls]([id_hall])
ON UPDATE CASCADE
GO
ALTER TABLE [clients_movies_halls] CHECK CONSTRAINT [clients_movies_halls_fk2]
GO

