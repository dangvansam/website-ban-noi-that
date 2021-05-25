(function ($) {
        $.fn.tblToExcel = function () {
            var elm = true;
            if (this.length > 1) {
				$('body').append('<div id="tbl-tnv-back" style="position: fixed; z-index: 1;padding-top: 100px;left: 0;top: 0;width: 100%;height: 100%;overflow: auto;background-color: rgb(0,0,0);background-color: rgba(0,0,0,0.4);">' +
				'<div id="tbl-tnv-excel" style="background-color: #fefefe;margin: auto;' +
				'padding: 20px; ' +
                'overflow: auto;' +
				'border: 1px solid #888;' +
				'width: 80%;" >  </div>' +
				'</div>');
                elm = false;
            }
            $('#tbl-tnv-back').click(function () {
                $(this).remove();
                $('#tbl-tnv-anch').remove();
            });
            var tableToExcel = (function () {
                var i = 0;
                var uri = 'data:application/vnd.ms-excel;base64,',
                    template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
                    , base64 = function (s) {
                        return window.btoa(unescape(encodeURIComponent(s)))
                    }
                    , format = function (s, c) {
                        return s.replace(/{(\w+)}/g, function (m, p) {
                            return c[p];
                        })
                    };
                return function (table, name) {
                    if (!table.nodeType) table
                    var ctx = {worksheet: name || 'Worksheet', table: table.innerHTML}
                    if (elm) {
                        window.location.href = uri + base64(format(template, ctx));
                    } else {
                        i++;
                        var xl = uri + base64(format(template, ctx));
                        $('#tbl-tnv-excel').append('<a id="tbl-tnv-anch" style="background-color: #4CAF50;border: none;\n' +
                            'color: white;' +
                            'padding: 15px 32px;' +
                            'text-align: center;' +
                            'text-decoration: none;' +
                            'display: inline-block; margin: 1px;' +
                            'font-size: 16px;" href='+xl+' download>Download Excel-'+i+' </a>');
                    }
                }
            })();

            return this.each(function () {
                tableToExcel(this, 'W3C Example Table');
            });
        }

    }(jQuery));
