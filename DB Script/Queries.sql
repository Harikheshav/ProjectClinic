create database ClinicDb
use ClinicDb
create table frontoffice(username varchar(10),firstname varchar(25),lastname varchar(25),password varchar(30))
select * from frontoffice
create table doctor(doctor_id  int identity(1,1) primary key,firstname varchar(25),lastname varchar(25), sex varchar(1), specialisation varchar(25), _from int,_to int)
select * from doctor
create table patient(patient_id  int identity(1,1) primary key ,firstname varchar(25),lastname varchar(25), sex varchar(1), age int, dob Date)
select * from patient
create table appointment(appid int identity(1,1) primary key,patient_id  int foreign key references patient(patient_id) ,doctor_id int foreign key references doctor(doctor_id), visitdate Date, _from int,_to int)
select * from Doctor where specialisation = 'Orthopedics' --Not Working
select * from Appointment