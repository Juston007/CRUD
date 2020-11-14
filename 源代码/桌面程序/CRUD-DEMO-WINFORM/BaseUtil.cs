using HelpUtil;
using System;
using System.Collections;
using System.Data.SqlClient;

namespace CRUD_DEMO
{
    /// <summary>
    /// 该类封装了业务逻辑
    /// </summary>
    class BaseUtil
    {
        /// <summary>
        /// 查询指定id的学生信息
        /// </summary>
        public static String queryStudentById(String id)
        {
            return null;
        }

        /// <summary>
        /// 查询指定姓名的学生信息
        /// </summary>
        public static String[] queryStudentByName(String name)
        {
            return null;
        }


        /// <summary>
        /// 查询所有学生信息
        /// </summary>
        public static StudentEntity[] queryAllStudent()
        {
            //执行查询数据并获取结果
            ArrayList queryRes = SQLUtil.rawQuery("select * from student_info", null);
            //如果查询结果大于0条 （如果有数据存在）
            if (queryRes.Count > 0)
            {
                //计数变量
                int i = 0;
                //学生实体数组
                StudentEntity[] entities = new StudentEntity[queryRes.Count];

                foreach (Hashtable table in queryRes)
                {
                    //从哈希表中取出对应数据并赋值给学生实体
                    StudentEntity entity = new StudentEntity();

                    entity.StuID = table["stuID"].ToString().Trim();
                    entity.StuName = table["stuName"].ToString().Trim();
                    entity.StuAge = int.Parse(table["stuAge"].ToString().Trim());
                    entity.StuSex = (bool)table["stuSex"];
                    entity.StuAddress = table["stuAddress"].ToString().Trim();

                    //将学生实体赋值给学生实体数组
                    entities[i++] = entity;
                }
                return entities;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 通过id删除学生
        /// </summary>
        /// <returns></returns>
        public static bool deleteStudentById(String id)
        {
            //执行删除sql命令 返回值为执行此语句而受影响的行数
            int line = SQLUtil.excuteSQL("Delete student_info where stuID = @stuID", new SqlParameter[] { new SqlParameter("@stuID", id) });
            return line >= 1;
        }

        /// <summary>
        /// 增加学生
        /// </summary>
        /// <returns></returns>
        public static bool addStudent(StudentEntity entity)
        {
            if (entity == null)
                return false;

            //INSERT INTO student_info ([stuID],[stuName] ,[stuAge],[stuSex],[stuAddress]) VALUES (@stuID,@stuName,@stuAge,@stuSex,@stuAddress

            //这些都是参数 在下方sql语句会替代对应的占位符  例如替代sql语句中 @stuID 为 entity.StuID的内容
            SqlParameter[] parameter = new SqlParameter[5];
            parameter[0] = new SqlParameter("@stuID", entity.StuID);
            parameter[1] = new SqlParameter("@stuName", entity.StuName);
            parameter[2] = new SqlParameter("@stuAge", entity.StuAge);
            parameter[3] = new SqlParameter("@stuSex", entity.StuSex);
            parameter[4] = new SqlParameter("@stuAddress", entity.StuAddress);

            //执行添加sql命令 返回值为执行此语句而受影响的行数
            int line = SQLUtil.excuteSQL("INSERT INTO student_info ([stuID],[stuName] ,[stuAge],[stuSex],[stuAddress]) VALUES (@stuID,@stuName,@stuAge,@stuSex,@stuAddress)", parameter);
            return line >= 1;
        }

        /// <summary>
        /// 更新学生信息
        /// </summary>
        /// <returns></returns>
        public static bool updateStudentInfo(StudentEntity entity)
        {
            //Update student_info SET stuName = @stuID,stuAge = @stuName,stuSex = @stuSex,stuAddress = @stuAddress Where StuID = @stuID
            SqlParameter[] parameter = new SqlParameter[5];
            parameter[0] = new SqlParameter("@stuID", entity.StuID);
            parameter[1] = new SqlParameter("@stuName", entity.StuName);
            parameter[2] = new SqlParameter("@stuAge", entity.StuAge);
            parameter[3] = new SqlParameter("@stuSex", entity.StuSex);
            parameter[4] = new SqlParameter("@stuAddress", entity.StuAddress);

            //执行更新sql命令 返回值为执行此语句而受影响的行数
            int line = SQLUtil.excuteSQL("Update student_info SET stuName = @stuName,stuAge = @stuAge,stuSex = @stuSex,stuAddress = @stuAddress Where StuID = @stuID", parameter);
            return line >= 1;
        }
    }
}
