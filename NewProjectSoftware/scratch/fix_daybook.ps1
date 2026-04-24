$path = 'c:\Repository\NewProjectSoftware\NewProjectSoftware\Controllers\AdminController.cs'
$content = Get-Content $path
$newContent = @()
foreach ($line in $content) {
    if ($line -like '*ViewBag.TotalCRAmount = double.Parse(ds.Tables[0].Compute("sum(CrAmount)", "").ToString()).ToString("0.00");*') {
        $newContent += '                object sumCr = ds.Tables[0].Compute("sum(CrAmount)", "");'
        $newContent += '                ViewBag.TotalCRAmount = (sumCr != DBNull.Value && sumCr.ToString() != "") ? double.Parse(sumCr.ToString()).ToString("0.00") : "0.00";'
    }
    elseif ($line -like '*ViewBag.TotalDRAmount = double.Parse(ds.Tables[0].Compute("sum(DrAmount)", "").ToString()).ToString("0.00");*') {
        $newContent += '                object sumDr = ds.Tables[0].Compute("sum(DrAmount)", "");'
        $newContent += '                ViewBag.TotalDRAmount = (sumDr != DBNull.Value && sumDr.ToString() != "") ? double.Parse(sumDr.ToString()).ToString("0.00") : "0.00";'
    }
    else {
        $newContent += $line
    }
}
Set-Content $path $newContent
