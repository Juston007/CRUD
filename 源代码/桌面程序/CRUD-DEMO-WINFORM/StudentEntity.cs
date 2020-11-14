using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_DEMO
{
    //学生实体 对应着数据库中的一条数据
    class StudentEntity
    {
        private String stuID, stuName, stuAddress;
        private bool stuSex;
        private int stuAge;

        public string StuID { get => stuID; set => stuID = value; }
        public string StuName { get => stuName; set => stuName = value; }
        public string StuAddress { get => stuAddress; set => stuAddress = value; }
        public bool StuSex { get => stuSex; set => stuSex = value; }
        public int StuAge { get => stuAge; set => stuAge = value; }
    }
}
