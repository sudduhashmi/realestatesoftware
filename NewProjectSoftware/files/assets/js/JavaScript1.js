
    function ExportToExcel(type, fn, dl) {
        var elt = document.getElementById('tbl_exporttable_to_xls');
    var wb = XLSX.utils.table_to_book(elt, {sheet: "sheet1" });
    return dl ?
    XLSX.write(wb, {bookType: type, bookSST: true, type: 'base64' }) :
    XLSX.writeFile(wb, fn || ('MySheetName.' + (type || 'xlsx')));
    }
    function searching() {
        var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("tbl_exporttable_to_xls");
    tr = table.getElementsByTagName("tr");

    for (i = 0; i < tr.length; i++) {
            var nameCol = tr[i].getElementsByTagName("td")[2]; // Name column (index 2)
    var mobileCol = tr[i].getElementsByTagName("td")[4]; // Mobile column (index 4)
    var cityCol = tr[i].getElementsByTagName("td")[8]; // City column (index 8)

    if (nameCol && mobileCol && cityCol) {
                var nameValue = nameCol.textContent || nameCol.innerText;
    var mobileValue = mobileCol.textContent || mobileCol.innerText;
    var cityValue = cityCol.textContent || cityCol.innerText;

                if (nameValue.toUpperCase().indexOf(filter) > -1 ||
                    mobileValue.toUpperCase().indexOf(filter) > -1 ||
                    cityValue.toUpperCase().indexOf(filter) > -1) {
        tr[i].style.display = "";
                } else {
        tr[i].style.display = "none";
                }
            }
        }
    }
    $(document).ready(function () {
        $('#tbl_exporttable_to_xls').DataTable({
            "iDisplayLength": 10,
            "buttons": [
                {
                    extend: 'excel',
                    exportOptions: {
                        modifier: {
                            page: 'all'
                        }
                    }
                }
            ]
        });
    });
    function exportToPDF() {
        var doc = new jsPDF();

    // Add the table using jsPDF autoTable plugin
    doc.autoTable({html: '#tbl_exporttable_to_xls' });

    // Save the PDF file
    doc.save('table.pdf');
    }
