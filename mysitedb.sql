create table bs_posts(
	post_id int identity(1,1) primary key,
	post_name varchar(500),
	post_cover varchar(max),
	post_content varchar(max),
	post_date varchar(max),
	post_category varchar(500),
	post_slug varchar(max)
);

create table bs_category(
	cat_id int identity(1,1) primary key,
	cat_name varchar(500)
);