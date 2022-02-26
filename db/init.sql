CREATE DATABASE prescription_db;
\connect prescription_db

CREATE TABLE IF NOT EXISTS login_info(
   id SERIAL PRIMARY KEY,
   username varchar(64) NOT NULL,
   password varchar(64) NOT NULL,
   salt varchar(32) NOT NULL
);

CREATE TABLE IF NOT EXISTS address(
   id SERIAL PRIMARY KEY,
   streetname varchar(64) NOT NULL,
   streetnumber varchar(8) NOT NULL,
   zipcode varchar(32) NOT NULL
);

CREATE TABLE IF NOT EXISTS pharmacy(
   id SERIAL PRIMARY KEY,
   pharmacy_name varchar(64) NOT NULL,
   address_id int,
   CONSTRAINT fk_address
      FOREIGN KEY(address_id) 
	  REFERENCES address(id)
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
   last_name varchar(64) NOT NULL,
   login_id int,
   role_id int,
   CONSTRAINT fk_login
      FOREIGN KEY(login_id) 
	  REFERENCES login_info(id),
   CONSTRAINT fk_role
      FOREIGN KEY(role_id) 
	  REFERENCES user_role(id)
);

CREATE TABLE IF NOT EXISTS patient(
   id SERIAL PRIMARY KEY
) INHERITS (person);


CREATE TABLE IF NOT EXISTS pharmaceut(
   id SERIAL PRIMARY KEY,
   pharmacy_id int,
   CONSTRAINT fk_pharmacy
      FOREIGN KEY(pharmacy_id) 
	  REFERENCES pharmacy(id)
) INHERITS (person);

CREATE TABLE IF NOT EXISTS doctor(
   id SERIAL PRIMARY KEY
) INHERITS (person);

CREATE TABLE IF NOT EXISTS medicine(
   id SERIAL PRIMARY KEY,
   name varchar(64) NOT NULL
);

CREATE TABLE IF NOT EXISTS prescription(
   id BIGSERIAL PRIMARY KEY,
   expiration DATE,
   creation TIMESTAMP NOT NULL,
   medicine_id int NOT NULL,
   prescribed_by int NOT NULL,
   prescribed_to int NOT NULL,
   last_administered_by int NOT NULL,
   CONSTRAINT fk_medicine
      FOREIGN KEY(medicine_id) 
	   REFERENCES medicine(id),
   CONSTRAINT fk_prescriber
      FOREIGN KEY(prescribed_by) 
	   REFERENCES doctor(id),
   CONSTRAINT fk_prescribee
      FOREIGN KEY(prescribed_to) 
	   REFERENCES patient(id),
   CONSTRAINT fk_administered_by
      FOREIGN KEY(last_administered_by) 
	   REFERENCES pharmaceut(id)
);


CREATE TABLE IF NOT EXISTS user_role_user_permission(
   role_id int,
   permission_id int,
   PRIMARY KEY(role_id, permission_id),
   CONSTRAINT fk_role
      FOREIGN KEY(role_id) 
	   REFERENCES user_role(id),
   CONSTRAINT fk_permission
      FOREIGN KEY(permission_id) 
	   REFERENCES permission(id),
);

