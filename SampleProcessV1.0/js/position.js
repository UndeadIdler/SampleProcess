// JScript 文件


    function show()
    {
        var detail=document.getElementById('detail');
        detail.style.top=0;
        detail.style.left=0;

        detail.style.display="block";
    }
    //第一个详细页面的显示（报告中的样品信息列表）
    function showAddEdit()
    {
        var detail = document.getElementById('detail');

        
        detail.style.display = "block";
    }
    //第一个详细页面的显示（报告中的样品信息列表）
    function hiddenDetail()//隐藏详细信息层
    {
        document.all('detail').style.display="none";
    }
    
    // DetailAnalysis层 JScript 文件

    function showAnalysis()
    {
        var DetailAnalysis = document.getElementById('DetailAnalysis');
        DetailAnalysis.style.top = 0;
        DetailAnalysis.style.left = 0;

        DetailAnalysis.style.display = "block";
    }
    function showAddEditAnalysis()
    {
        var DetailAnalysis = document.getElementById('DetailAnalysis');
        DetailAnalysis.style.top = 0;
        DetailAnalysis.style.left = 0;
        DetailAnalysis.style.top = parseInt(document.documentElement.scrollHeight) / 3 - parseInt(DetailAnalysis.style.height) / 3;
        DetailAnalysis.style.left = parseInt(document.documentElement.scrollWidth) / 3 - parseInt(DetailAnalysis.style.width) / 3;
        DetailAnalysis.style.display = "block";
    }
    function unshowAddEditAnalysis()//隐藏详细信息层
    {
        document.all('DetailAnalysis').style.display = "none";
    }
    function hiddenDetailAnalysis()//隐藏详细信息层
    {
        document.getElementById('DetailAnalysis').style.display = "none";
    }
    
     // DetailAnalysisAdd层 JScript 文件
    function showDetailAnalysisAdd()
    {
       document.all('DetailAnalysisAdd').style.display="block";
   }
    function showAnalysisAdd()
    {
        var DetailAnalysisAdd = document.getElementById('DetailAnalysisAdd');
        DetailAnalysisAdd.style.top = 0;
        DetailAnalysisAdd.style.left = 0;
        DetailAnalysisAdd.style.display = "block";
    }
    function showAddEditAnalysisAdd()
    {

        var DetailAnalysisAdd = document.getElementById('DetailAnalysisAdd');
        DetailAnalysisAdd.style.display = "block";
    }

   
    function hiddenDetailAnalysisAdd()//隐藏详细信息层
    {
        document.all('DetailAnalysisAdd').style.display="none";
    }
    function DrawShow() {

        var DetailAnalysisItem = document.getElementById('Add');

       
        DetailAnalysisItem.style.display = "block";
    }
    function unshowDraw() {
        var DetailAnalysisItem = document.getElementById('Add');

        DetailAnalysisItem.style.display = "none";
    }
   
    function showprintchose() {
        var detail = document.getElementById('detail');

        detail.style.top = document.body.scrollTop + 300;
        detail.style.left = document.body.scrollLeft + 300;
        detail.style.display = "block";

    }
    function unshowprintchose() {
        document.all('detail').style.display = "none";
    }
    function showchose() {
        var detail = document.getElementById('div_choose');

        detail.style.display = "block";

    }
    function unshowchose()
    {
        var detail = document.getElementById('div_choose');

        detail.style.display = "none";
    }

   