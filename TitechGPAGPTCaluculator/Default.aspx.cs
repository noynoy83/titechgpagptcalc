using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TitechGPAGPTCalculator
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private bool CalcGPs()
        {
            var l = ScoresTextBox.Text.Replace("\r", "").Split("\n".ToCharArray());
            foreach (var ll in l)
            {
                var line = ll.Trim();
                if (string.IsNullOrEmpty(line)) { continue; }
                var sp = line.Split('\t');
                if (!(sp.Length == 7 || sp.Length == 8)) { continue; }
                int score = -1;
                double gp = -1;
                int flag = 0;
                if (sp.Length == 8) flag = 1;

                // Credits
                double credits = 0;
                var c_a = sp[3 + flag].Split('-');
                credits = double.Parse(c_a[0]) + double.Parse(c_a[1]) + double.Parse(c_a[2]);

                if (int.TryParse(sp[4 + flag], out score))
                {
                    // if score is valid:
                    if (0 <= score && score <= 100)
                    {
                        gp = (score - 55.0) / 10.0;
                        if (score < 60)
                        {
                            gp = 0;
                            Session["sum_of_failure_credits"] = (double)Session["sum_of_failure_credits"] + credits;
                        }
                        Session["sum_of_gp"] = (double)Session["sum_of_gp"] + gp * credits;
                    }
                    // illegal range
                    else
                    {
                        Session["errtxt"] = sp[1 + flag];
                        return false;
                    }
                }
                else if (sp[4 + flag] == "合格" || sp[4 + flag] == "認定")
                {
                    score = 80;
                    gp = 2.5;
                    Session["sum_of_gp"] = (double)Session["sum_of_gp"] + gp * credits;
                    if (sp[4 + flag] == "認定")
                    {
                        Session["sum_of_nintei_credits"] = (double)Session["sum_of_nintei_credits"] + credits;
                    }
                }
                else if (sp[4 + flag] == "不合格")
                {
                    score = 0;
                    gp = 0;
                    Session["sum_of_failure_credits"] = (double)Session["sum_of_failure_credits"] + credits;
                }
                else if (sp[4 + flag] == "未報告")
                {
                    // do nothing
                    continue;
                }
                else
                {
                    Session["errtxt"] = sp[1 + flag];
                    return false;
                }

                // unconsider for GPA
                if (sp[4 + flag] == "合格" || sp[4 + flag] == "認定" || sp[4 + flag] == "不合格" ||
                    sp[1 + flag].Contains("研究プロジェクト") || sp[1 + flag].Contains("B2D"))
                {
                    Session["sum_of_gp_unconsidering_for_gpa"] = (double)Session["sum_of_gp_unconsidering_for_gpa"] + gp * credits;
                    Session["sum_of_credits_unconsidering_for_gpa"] = (double)Session["sum_of_credits_unconsidering_for_gpa"] + credits;
                }
                Session["sum_of_credits"] = (double)Session["sum_of_credits"] + credits;
            }

            return true;
        }

        protected void CheckButton_Click(object sender, EventArgs e)
        {
            Session["sum_of_gp"] = 0d;
            Session["sum_of_credits"] = 0d;
            Session["sum_of_failure_credits"] = 0d;
            Session["sum_of_nintei_credits"] = 0d;
            Session["sum_of_gp_unconsidering_for_gpa"] = 0d;
            Session["sum_of_credits_unconsidering_for_gpa"] = 0d;
            Session["errtxt"] = "";

            if (!(CheckBox1.Checked))
            {
                CheckResultTextBox.Text = $"Failure: チェックボックスを確認してください．";
                return;
            }

            if (!CalcGPs())
            {
                CheckResultTextBox.Text = $"Failure: 不正な行があります．/該当科目：{Session["errtxt"]}";
                return;
            }
            CheckResultTextBox.Text = $"Passed:\r\n" +
                $"・教務ウェブGPT（「認定」科目を含まない）：{((double)Session["sum_of_gp"] - (double)Session["sum_of_nintei_credits"] * 2.5) / 110.0}\r\n" +
                $"・電電GPT（「認定」科目を含む）: {(double)Session["sum_of_gp"] / 110.0} \r\n" +
                $"・GPA参考値(「認定」・「合否」・研究関連科目も含む): {(double)Session["sum_of_gp"] / (double)Session["sum_of_credits"]} \r\n" +
                $"・GPA（「認定」・「合否」科目/『研究プロジェクト』・『B2D』科目除く）: {((double)Session["sum_of_gp"] - (double)Session["sum_of_gp_unconsidering_for_gpa"]) / ((double)Session["sum_of_credits"] - (double)Session["sum_of_credits_unconsidering_for_gpa"])} \r\n" +
                $"・履修申告単位数: {Session["sum_of_credits"]} \r\n" +
                $"・合格（60点以上/「合格」/「認定」）単位数：{(double)Session["sum_of_credits"] - (double)Session["sum_of_failure_credits"]} of {Session["sum_of_credits"]} \r\n" +
                $"・GPA（「認定」・「合否」科目/『研究プロジェクト』・『B2D』科目除く）で考慮した単位数: {((double)Session["sum_of_credits"] - (double)Session["sum_of_credits_unconsidering_for_gpa"])} \r\n\r\n" +
                "※「未報告」科目については計算処理を飛ばす（あらゆる計算で考慮しない）としています．\r\n" +
                "※『GPA（「認定」・「合否」科目/『研究プロジェクト』・『B2D』科目除く）で考慮した単位数』には，不合格（60点未満）の単位も含まれた表示が出ています．";
            Session["passedtext"] = ScoresTextBox.Text;
        }
    }
}