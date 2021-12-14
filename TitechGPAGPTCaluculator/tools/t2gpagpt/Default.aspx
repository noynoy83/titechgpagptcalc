<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TitechGPAGPTCalculator.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Titech GPA/GPT Calculator</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Titech GPA/GPT計算機</h1>

            <h2>このページでできること</h2>
            <p>入力されたデータだけを元にして</p>
            <ul>
                <li>電電GPT（「認定」科目を含む）</li>
                <li>教務ウェブGPT（「認定」科目を含まない）</li>
                <li>GPA参考値(「認定」・「合否」・研究関連科目も含む)</li>
                <li>GPA（「認定」・「合否」科目/『研究プロジェクト』・『B2D』科目除く</li>
                <li>履修申告単位数</li>
                <li>合格（60点以上/「合格」/「認定」）単位数</li>
                <li>GPA（「認定」・「合否」科目/『研究プロジェクト』・『B2D』科目除く）で考慮した単位数</li>
            </ul>
            <p>
                を表示できます．</p>
            <p>
                　主に，1Qや3Qの成績開示時のGPA/GPT計算や，各QごとのGPTの計算，電電の人が「認定」科目を含めたGPTを知る</p>
            <p>
                のに役立つ気がします．</p>
            <p>
                ※ バグ報告はTwitter（<a href="https://twitter.com/noynoy83">@noynoy83</a>）か（修正いただける方は）<a href="https://github.com/noynoy83/titechgpagptcalc">Github</a>のPull Reqでお願いします．</p>

            <h2>入力方法</h2>
            <p>1. 「<strong>習得時期別成績一覧</strong>」または「<strong>成績閲覧</strong>」ページから成績全体を選択し，「コピー」します．</p>
            <p>2. 続けて，テキストボックス欄に<strong>そのまま</strong>貼り付けます．以下のような画像の手順を踏めば問題ありません．</p>
            <p>注意：「GPA・GPT」のページ<strong>ではなく</strong>，かならず「<strong>成績閲覧</strong>」か「<strong>修得時期別成績一覧</strong>」のページを参照してください．</p>
            <p>補足：入力していただいた成績は，計算を行った後，即破棄されます．（<strong>サーバにデータは残りません</strong>）</p>
            <p>※参考動画</p>
            <blockquote class="twitter-tweet"><p lang="ja" dir="ltr">GPTとGPAを秒で計算するサイト作った<br>（🐌GPTアンケ用に認定科目もGP2.5にしてたりという仕様なので一般公開はしない） <a href="https://t.co/7zYjQGl7Fd">pic.twitter.com/7zYjQGl7Fd</a></p>&mdash; のい🐌 (@noynoy83) <a href="https://twitter.com/noynoy83/status/1469623775675453445?ref_src=twsrc%5Etfw">December 11, 2021</a></blockquote> <script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
            
            <h2>成績入力欄</h2>
            <p><strong>注意：教職科目など，卒業要件とならない科目の行はご自身で取り除いてから送信してください．</strong></p>
            <p>※研究プロジェクトとB2D科目以外の研究関連科目はGPA計算で除外していません．必要ならば個人で適宜取り除いてください．</p>
            <p>（なお，以上に該当しなければ何も編集の必要はありません．「未報告」科目の行も特に取り除く必要はありません．）</p>
            <asp:TextBox ID="ScoresTextBox" runat="server" Width="100%" TextMode="MultiLine" Wrap="False" Rows="20" ></asp:TextBox>

            <h2>計算</h2>
            <p>計算結果やエラー有無について，表示されます．"Passed"の表示が出ればエラーは検出されていません．</p>
            <p>計算結果に間違いなどあれば，作者(<a href="https://twitter.com/noynoy83">@noynoy83</a>)にバグ報告をくださるか<a href="https://github.com/noynoy83/titechgpagptcalc">Github</a>にPull Reqしてもらえると助かります．</p>
            <asp:CheckBox ID="CheckBox1" Text="教職科目（卒業要件とならない科目）に関する行は取り除いた or もともとない．" runat="server" />
            <br />
            <br />
            <asp:Button ID="CheckButton" runat="server" Text="計算実行" OnClick="CheckButton_Click" />

            <p>計算結果</p>
            <asp:TextBox ID="CheckResultTextBox" ReadOnly="true" TextMode="MultiLine" runat="server" Width="100%" Rows="10"></asp:TextBox>

            <h2>ライセンスなど</h2>
            <p>このプログラムのライセンスは<a href="https://opensource.org/licenses/mit-license.php">MIT License</a>を採用し，オープンソースで公開されます．</p>
            <p>Copyright (c) 2021 のい / This program is released under <a href="https://opensource.org/licenses/mit-license.php">the MIT license</a>.</p>
            <p>ソースコード： <a href="https://github.com/noynoy83/titechgpagptcalc">https://github.com/noynoy83/titechgpagptcalc</a></p>
        </div>
    </form>
</body>
</html>
