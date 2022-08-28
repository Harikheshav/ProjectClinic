create database ClinicDb
use ClinicDb
create table frontoffice(username varchar(10),firstname varchar(25),lastname varchar(25),password varchar(30))
select * from frontoffice
create table doctor(firstname varchar(25),lastname varchar(25), sex varchar(1), specialisation varchar(25), visitinghrs int)
select * from doctor
create table patient(patient_id int primary key,firstname varchar(25),lastname varchar(25), sex varchar(1), age int, dob Date)
select * from patient
drop table patient