path = r'c:\Repository\NewProjectSoftware\NewProjectSoftware\Controllers\AdminController.cs'
with open(path, 'r') as f:
    lines = f.readlines()

new_lines = []
for line in lines:
    if 'ViewBag.TotalCRAmount = double.Parse(ds.Tables[0].Compute("sum(CrAmount)", "").ToString()).ToString("0.00");' in line:
        new_lines.append('                object sumCr = ds.Tables[0].Compute("sum(CrAmount)", "");\n')
        new_lines.append('                ViewBag.TotalCRAmount = (sumCr != System.DBNull.Value && sumCr.ToString() != "") ? double.Parse(sumCr.ToString()).ToString("0.00") : "0.00";\n')
    elif 'ViewBag.TotalDRAmount = double.Parse(ds.Tables[0].Compute("sum(DrAmount)", "").ToString()).ToString("0.00");' in line:
        new_lines.append('                object sumDr = ds.Tables[0].Compute("sum(DrAmount)", "");\n')
        new_lines.append('                ViewBag.TotalDRAmount = (sumDr != System.DBNull.Value && sumDr.ToString() != "") ? double.Parse(sumDr.ToString()).ToString("0.00") : "0.00";\n')
    else:
        new_lines.append(line)

with open(path, 'w') as f:
    f.writelines(new_lines)
