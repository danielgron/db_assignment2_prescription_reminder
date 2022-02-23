CREATE DATABASE prescription_db;
\connect prescription_db

CREATE TABLE IF NOT EXISTS login_info(
   id SERIAL PRIMARY KEY,
   username varchar(64) NOT NULL,
   password varchar(64) NOT NULL,
   salt varchar(32) NOT NULL
);

CREATE TABLE IF NOT EXISTS pharmacy(
   id SERIAL PRIMARY KEY,
   pharmacy_name varchar(64) NOT NULL
);

CREATE TABLE IF NOT EXISTS user_role(
   id SERIAL PRIMARY KEY
);

CREATE TABLE IF NOT EXISTS user_permission(
   id SERIAL PRIMARY KEY
);


CREATE TABLE IF NOT EXISTS person(
   id SERIAL PRIMARY KEY,
   first_name varchar(64) NOT NULL,
   last_name varchar(64) NOT NULL
);

CREATE TABLE IF NOT EXISTS patient(
   id SERIAL PRIMARY KEY
) INHERITS (person);


CREATE TABLE IF NOT EXISTS pharmaceut(
   id SERIAL PRIMARY KEY,
   pharmacy_id bigint,
   CONSTRAINT fk_pharmacy
      FOREIGN KEY(pharmacy_id) 
	  REFERENCES pharmacy(id)
);

CREATE TABLE IF NOT EXISTS doctor(
   id SERIAL PRIMARY KEY
);

CREATE TABLE IF NOT EXISTS medicine(
   id SERIAL PRIMARY KEY,
   name varchar(64) NOT NULL
);

CREATE TABLE IF NOT EXISTS prescription(
   id SERIAL PRIMARY KEY,
   expiration DATE,
   creation TIMESTAMP
);


CREATE TABLE IF NOT EXISTS user_role_user_permission(
   role_id bigint,
   permission_id bigint,
   PRIMARY KEY(role_id, permission_id)
);

