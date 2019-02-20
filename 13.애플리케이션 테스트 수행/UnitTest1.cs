using System;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private SqlConnection conn;
        public bool status;

        public UnitTest1()
        {
            status = Connection();
        }

        [TestMethod]
        private bool Connection()
        {
            bool status = true;

            string host = "(localdb)\\ProjectsV13";
            string user = "root";
            string password = "1234";
            string db = "gdc";

            string connStr = string.Format("server={0};uid={1};password={2};database={3}", host, user, password, db);
            SqlConnection conn = new SqlConnection(connStr);

            try
            {
                conn.Open();
                this.conn = conn;
                status = true;
                Assert.AreEqual(true, status);
                return true;
            }
            catch
            {
                conn.Close();
                this.conn = null;
                status = false;
                Assert.AreEqual(true, status);
                return false;
            }
        }

        [TestMethod]
        public void NonQuery()
        {
            string sql = "aaaa";
            bool status;
            try
            {
                if (true)
                {
                    SqlCommand comm = new SqlCommand(sql, conn);
                    comm.ExecuteNonQuery();
                    status = true;
                    Assert.AreEqual(true, status);
                    //return true;
                }
                else
                {
                    //return false;
                }
            }
            catch
            {
                status = false;
                Assert.AreEqual(true, status);
                //return false;
            }
        }

        [TestMethod]
        public void Reader()
        {
            string sql = "select * from Member;";
            try
            {
                if (status)
                {
                    SqlCommand comm = new SqlCommand(sql, conn);
                    comm.ExecuteReader();
                    Console.WriteLine("읽어오기 성공");
                }
                else
                {
                }
            }
            catch
            {
                Console.WriteLine("읽어오기 실패");
            }
        }

        [TestMethod]
        public void SelectData()
        {
            int count = 0;

            string sql = "select mNo, mID, mPass, mName, delYn, regDate, modDate from Member;";
            SqlDataReader sdr = Reader(sql);
            while (sdr.Read())
            {
                count++;
            }
            Assert.AreEqual(6, count);
        }

        [TestMethod]
        public void InsertData()
        {
            int status;
            string sql = "insert into Member (mID, mPass, mName) values ('','','');";
            status = NonQuery(sql);
            Assert.AreEqual(1, status);
        }

        [TestMethod]
        public void UpdateData()
        {
            int status=0;
            string sql = "update Member set mID = 'test', mPass = '0000', mName = 'test', modDate = getDate() where mNo = 50";
            status = NonQuery(sql);
            Assert.AreEqual(1, status);
        }

        [TestMethod]
        public void DeleteData()
        {
            int status = 0;
            string sql = "update Member set delYn = 'Y' where mNo = 1";
            status = NonQuery(sql);
            Assert.AreEqual(1, status);
        }



        //public bool NonQuery(string sql)
        //{
        //    try
        //    {
        //        if (status)
        //        {
        //            SqlCommand comm = new SqlCommand(sql, conn);
        //            comm.ExecuteNonQuery();
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}


        public int NonQuery(string sql)
        {
            try
            {
                if (status)
                {
                    SqlCommand comm = new SqlCommand(sql, conn);
                    return comm.ExecuteNonQuery();
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                return -1;
            }
        }

        public SqlDataReader Reader(string sql)
        {
            try
            {
                if (status)
                {
                    SqlCommand comm = new SqlCommand(sql, conn);
                    return comm.ExecuteReader();
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
