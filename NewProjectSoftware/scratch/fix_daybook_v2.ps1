$path = 'c:\Repository\NewProjectSoftware\NewProjectSoftware\Controllers\AdminController.cs'
$content = Get-Content $path
$old1 = '                ViewBag.TotalCRAmount = double.Parse(ds.Tables[0].Compute("sum(CrAmount)", "").ToString()).ToString("0.00");'
$new1 = '                object sumCr = ds.Tables[0].Compute("sum(CrAmount)", "");' + "`r`n" + '                ViewBag.TotalCRAmount = (sumCr != DBNull.Value && sumCr.ToString() != "") ? double.Parse(sumCr.ToString()).ToString("0.00") : "0.00";'
$old2 = '                ViewBag.TotalDRAmount = double.Parse(ds.Tables[0].Compute("sum(DrAmount)", "").ToString()).ToString("0.00");'
$new2 = '                object sumDr = ds.Tables[0].Compute("sum(DrAmount)", "");' + "`r`n" + '                ViewBag.TotalDRAmount = (sumDr != DBNull.Value && sumDr.ToString() != "") ? double.Parse(sumDr.ToString()).ToString("0.00") : "0.00";'

$newContent = @()
foreach ($line in $content) {
    if ($line.Trim() -eq $old1.Trim()) {
        $newContent += '                object sumCr = ds.Tables[0].Compute("sum(CrAmount)", "");'
        $newContent += '                ViewBag.TotalCRAmount = (sumCr != DBNull.Value && sumCr.ToString() != "") ? double.Parse(sumCr.ToString()).ToString("0.00") : "0.00";'
    }
    elseif ($line.Trim() -eq $old2.Trim()) {
        $newContent += '                object sumDr = ds.Tables[0].Compute("sum(DrAmount)", "");'
        $newContent += '                ViewBag.TotalDRAmount = (sumDr != DBNull.Value && sumDr.ToString() != "") ? double.Parse(sumDr.ToString()).ToString("0.00") : "0.00";'
    }
    else {
        $newContent += $line
    }
}
Set-Content $path $newContent
