using CRUD_DEMO;
using System;
using System.Windows.Forms;

namespace CRUD_DEMO_WINFORM
{
    /// <summary>
    /// By HaiChuan Wang
    /// Date：20201114
    /// 具体连接数据库的逻辑在SQLUtil.cs中
    /// StudentEntity为学生实体类，对应着数据库中的一条学生数据
    /// </summary>
    public partial class MainForm : Form
    {
        //当前正在展示的数据
        private StudentEntity[] currentDisplayData;
        //当前选中的序号
        private int currentSelectRowIndex = -1;

        public MainForm()
        {
            InitializeComponent();
        }

        //查询数据按钮点击事件
        private void btnQueryData_Click(object sender, EventArgs e)
        {
            //绑定数据源
            dataGridView.DataSource = (currentDisplayData = BaseUtil.queryAllStudent());
        }

        //添加数据按钮点击事件
        private void btnAddData_Click(object sender, EventArgs e)
        {
            String name = txtName.Text.ToString().Trim();
            String address = txtAddress.Text.ToString().Trim();
            String id = txtID.Text.ToString().Trim();
            int sexStr = cmbSex.SelectedIndex;
            String ageStr = txtAge.Text.ToString().Trim();
            
            //如果检查数据合法
            if(checkData())
            {
                bool sex = (sexStr == 0);
                int age;
                int.TryParse(ageStr, out age);
                
                //建立学生实体
                StudentEntity student = new StudentEntity();
                student.StuID = id;
                student.StuName = name;
                student.StuAddress = address;
                student.StuSex = sex;
                student.StuAge = age;

                //执行请求
                bool res = BaseUtil.addStudent(student);
                MessageBox.Show(res ? "添加成功" : "添加失败");
            }    
        }

        //删除数据按钮点击事件
        private void btnDelData_Click(object sender, EventArgs e)
        {
            if (!(currentSelectRowIndex >= 0))
            {
                MessageBox.Show("没有选中一个项！");
                return;
            }

            if (currentDisplayData != null)
            {
                bool isTure = BaseUtil.deleteStudentById(currentDisplayData[currentSelectRowIndex].StuID);
                MessageBox.Show(isTure ? "删除成功" : "删除失败");
                currentSelectRowIndex = -1;
            }
        }

        //gridView选中变动事件
        //在GridView选择变动时将数据赋值给相应的控件
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentSelectRowIndex = e.RowIndex;
            if (currentSelectRowIndex >= 0)
            {
                txtName.Text = currentDisplayData[currentSelectRowIndex].StuName;
                txtID.Text = currentDisplayData[currentSelectRowIndex].StuID;
                txtAddress.Text = currentDisplayData[currentSelectRowIndex].StuAddress;
                cmbSex.SelectedIndex = currentDisplayData[currentSelectRowIndex].StuSex ? 0 : 1;
                txtAge.Text = currentDisplayData[currentSelectRowIndex].StuAge.ToString();
            }
        }


        //检查数据是否合法
        private bool checkData()
        {
            String name = txtName.Text.ToString().Trim();
            String address = txtAddress.Text.ToString().Trim();
            String id = txtID.Text.ToString().Trim();
            String sexStr = cmbSex.SelectedIndex.ToString();
            String ageStr = txtAge.Text.ToString().Trim();

            if (name != String.Empty || address != String.Empty || id != String.Empty || sexStr != String.Empty || ageStr != String.Empty)
            {
                int age;
                if (!int.TryParse(ageStr, out age))
                {
                    MessageBox.Show("年龄应该是一个数字！");
                    return false;
                }
                return true;
            }
            else
            {
                MessageBox.Show("请填写完整！");
                return false;
            }

        }

        //WinForm初始化事件
        private void MainForm_Load(object sender, EventArgs e)
        {
            //默认选中索引为0的项
            cmbSex.SelectedIndex = 0;
        }

        //更新数据按钮点击事件
        private void btnUpdateData_Click(object sender, EventArgs e)
        {
            if(!(currentSelectRowIndex >= 0))
            {
                MessageBox.Show("没有选中一个项！");
                return;
            }

            String name = txtName.Text.ToString().Trim();
            String address = txtAddress.Text.ToString().Trim();
            String id = txtID.Text.ToString().Trim();
            int sexStr = cmbSex.SelectedIndex;
            String ageStr = txtAge.Text.ToString().Trim();


            if (checkData())
            {
                bool sex = (sexStr == 0);
                int age;
                int.TryParse(ageStr, out age);

                //建立学生实体
                StudentEntity student = new StudentEntity();
                student.StuID = currentDisplayData[currentSelectRowIndex].StuID;
                student.StuName = name;
                student.StuAddress = address;
                student.StuSex = sex;
                student.StuAge = age;

                //执行请求
                bool res = BaseUtil.updateStudentInfo(student);
                MessageBox.Show(res ? "更新成功" : "更新失败");
                currentSelectRowIndex = -1;
            }
        }

    }
}
