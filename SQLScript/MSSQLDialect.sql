create table CARD_SET (
	CARD_SET_ID int identity not null,
	DISPLAYNAME nvarchar(255) not null,
	DESCRIPTION nvarchar(max) null,
	CREATED_DATETIME datetime not null,
	MODIFIED_DATETIME datetime not null,
	CREATOR nvarchar(255) not null,
	MODIFIER nvarchar(255) not null,
	OWNER nvarchar(255) not null,
	STATE tinyint not null,
	primary key (CARD_SET_ID)
);
create table CARD (
	CARD_ID int identity not null,
	FLAG int not null,
	VOCABULARY nvarchar(255) not null,
	CREATED_DATETIME datetime not null,
	MODIFIED_DATETIME datetime not null,
	CREATOR nvarchar(255) not null,
	MODIFIER nvarchar(255) not null,
	STATE tinyint not null,
	primary key (CARD_ID)
);