using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace mySite.Models
{
    public class PostModel
    {
        public string post_id { get; set; }
        public string post_name { get; set; }
        public string post_cover { get; set; }
        public string post_content { get; set; }
        public string post_date { get; set; }
        public string post_category { get; set; }
        public string post_slug { get; set; }
        public string response { get; set; }

        SqlConnection conn;
        Utility ut = new Utility();

        public PostModel()
        {
            Config db = new Config();
            conn = new SqlConnection(db.getConnStr());
        }
        private bool openConn()
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }
        private bool closeConn()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }

        public void addPost()
        {
            string query = "insert into bs_posts(post_name, post_cover, post_content, post_date, post_category, post_slug) values ('" + ut.encode(this.post_name) + "', '" + ut.encode(this.post_cover) + "', '" + ut.encode(this.post_content) + "', '" + ut.encode(this.post_date) + "', '" + ut.encode(this.post_category) + "', '" + ut.encode(this.post_slug) + "');";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                closeConn();
                this.response = "200";
            }
            else
            {
                this.response = "500";
            }
        }
        public PostModel getPostById()
        {
            PostModel post = new PostModel();
            string query = "select * from bs_posts where post_id='" + this.post_id + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    post.post_name = ut.decode(reader["post_name"] + "");
                    post.post_cover = ut.decode(reader["post_cover"] + "");
                    post.post_content = ut.decode(reader["post_content"] + "");
                    post.post_date = ut.decode(reader["post_date"] + "");
                    post.post_category = ut.decode(reader["post_category"] + "");
                    post.post_slug = reader["post_slug"] + "";
                }

                reader.Close();
                closeConn();
                post.response = "200";
            }
            else
            {
                post.response = "500";
            }

            return post;
        }
        public PostModel getPostBySlug()
        {
            PostModel post = new PostModel();
            string query = "select * from bs_posts where post_slug='" + this.post_slug + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    post.post_id = ut.decode(reader["post_id"] + "");
                    post.post_name = ut.decode(reader["post_name"] + "");
                    post.post_cover = ut.decode(reader["post_cover"] + "");
                    post.post_content = ut.decode(reader["post_content"] + "");
                    post.post_date = ut.decode(reader["post_date"] + "");
                    post.post_category = ut.decode(reader["post_category"] + "");
                }

                reader.Close();
                closeConn();
                post.response = "200";
            }
            else
            {
                post.response = "500";
            }

            return post;
        }
        public List<PostModel> getAllPost()
        {
            List<PostModel> list = new List<PostModel>();
            string query = "select * from bs_posts";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new PostModel
                    {
                        post_id = ut.decode(reader["post_id"] + ""),
                        post_name = ut.decode(reader["post_name"] + ""),
                        post_cover = ut.decode(reader["post_cover"] + ""),
                        post_content = ut.decode(reader["post_content"] + ""),
                        post_date = ut.decode(reader["post_date"] + ""),
                        post_category = ut.decode(reader["post_category"] + ""),
                        post_slug = ut.decode(reader["post_slug"] + "")
                    });
                }

                reader.Close();
                closeConn();
                this.response = "200";
            }
            else
            {
                this.response = "500";
            }

            return list;
        }
        public List<PostModel> getAllPostByCat()
        {
            List<PostModel> list = new List<PostModel>();
            string query = "select * from bs_posts where post_category='" + this.post_category + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new PostModel
                    {
                        post_id = ut.decode(reader["post_id"] + ""),
                        post_name = ut.decode(reader["post_name"] + ""),
                        post_cover = ut.decode(reader["post_cover"] + ""),
                        post_content = ut.decode(reader["post_content"] + ""),
                        post_date = ut.decode(reader["post_date"] + ""),
                        post_slug = ut.decode(reader["post_slug"] + "")
                    });
                }

                reader.Close();
                closeConn();
                this.response = "200";
            }
            else
            {
                this.response = "500";
            }

            return list;
        }
        public List<PostModel> getAllPostByDesc()
        {
            List<PostModel> list = new List<PostModel>();
            string query = "select * from bs_posts order by post_id desc";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new PostModel
                    {
                        post_id = ut.decode(reader["post_id"] + ""),
                        post_name = ut.decode(reader["post_name"] + ""),
                        post_cover = ut.decode(reader["post_cover"] + ""),
                        post_content = ut.decode(reader["post_content"] + ""),
                        post_date = ut.decode(reader["post_date"] + ""),
                        post_slug = ut.decode(reader["post_slug"] + "")
                    });
                }

                reader.Close();
                closeConn();
                this.response = "200";
            }
            else
            {
                this.response = "500";
            }

            return list;
        }
        public int findPostMatches()
        {
            int result = 0;
            string query = "select count(post_slug) from bs_posts where post_slug like '%" + ut.encode(this.post_slug) + "%';";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                int count = int.Parse(cmd.ExecuteScalar() + "");
                result = count;
                closeConn();
                this.response = "200";
            }
            else
            {
                this.response = "500";
                result = 0;
            }

            return result;
        }
        public void updatePost()
        {
            string query = "update bs_posts set post_name='" + ut.encode(this.post_name) + "', post_cover='" + ut.encode(this.post_cover) + "', post_content='" + ut.encode(this.post_content) + "', post_category='" + this.post_category + "' where post_id='" + this.post_id + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                closeConn();
                this.response = "200";
            }
            else
            {
                this.response = "500";
            }
        }
        public void deletePost()
        {
            string query = "delete from bs_posts where post_id='" + this.post_id + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                closeConn();
                this.response = "200";
            }
            else
            {
                this.response = "500";
            }
        }
    }
}