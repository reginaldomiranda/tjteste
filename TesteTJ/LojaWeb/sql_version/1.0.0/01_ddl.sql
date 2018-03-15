create database vendas

use vendas

create table usuario(
usuario varchar(10),
senha varchar(10)
)

create table Cliente (
idcliente int not null,
cpf varchar(11),
nome varchar(30),
endereco varchar(30),
bairro varchar(30)

constraint pk_cliente primary key (idcliente)
)

create table produto (
idprd int not null,
descricao varchar(11),
marca   varchar(30),
vlrunit int

constraint pk_produto primary key (idprd)
)

create table vendas (
idvenda int not null,
idprd int not null,
idcliente int not null,
vlrunit int,
qtde float(10),
vlrTotal float(10),
flag char(1)

constraint pk_vendas primary key (idvenda)
)



