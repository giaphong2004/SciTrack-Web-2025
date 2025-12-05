// ===== HELPER FUNCTIONS CHO CHỨC NĂNG IN ẤN =====

/**
 * In thông tin chi tiết của một item
 * @param {string} moduleTitle - Tiêu đề module (VD: "Tài sản KH&CN")
 * @param {object} data - Dữ liệu cần in (key-value pairs)
 */
function printDetail(moduleTitle, data) {
    if (!data || Object.keys(data).length === 0) {
        alert('⚠️ Vui lòng chọn một mục để in!');
        return;
    }

    // Tạo HTML cho phần in
    let printContent = `
        <div class="print-section">
            <div class="print-header">
                <h3>PHẦN MỀM QUẢN LÝ KHOA HỌC & CÔNG NGHỆ</h3>
                <p><strong>${moduleTitle}</strong></p>
                <p>Ngày in: ${new Date().toLocaleDateString('vi-VN')} - ${new Date().toLocaleTimeString('vi-VN')}</p>
            </div>
            
            <div class="print-body">
                <h4>THÔNG TIN CHI TIẾT</h4>
                <table>
                    <tbody>
    `;

    // Thêm các trường thông tin
    for (let [key, value] of Object.entries(data)) {
        if (value !== null && value !== undefined && value !== '') {
            printContent += `
                <tr>
                    <td>${key}</td>
                    <td>${value}</td>
                </tr>
            `;
        }
    }

    printContent += `
                    </tbody>
                </table>
            </div>
            
            <div class="print-footer">
                <div class="print-signature">
                    <div>
                        <p><strong>Người lập phiếu</strong></p>
                        <p><em>(Ký và ghi rõ họ tên)</em></p>
                        <div class="signature-line"></div>
                    </div>
                    <div>
                        <p><strong>Người duyệt</strong></p>
                        <p><em>(Ký và ghi rõ họ tên)</em></p>
                        <div class="signature-line"></div>
                    </div>
                </div>
            </div>
        </div>
    `;

    // Xóa nội dung cũ nếu có
    let oldPrintSection = document.querySelector('.print-section');
    if (oldPrintSection) {
        oldPrintSection.remove();
    }

    // Thêm vào body
    document.body.insertAdjacentHTML('beforeend', printContent);

    // Đợi một chút để DOM cập nhật, sau đó in
    setTimeout(() => {
        window.print();
    }, 100);
}

/**
 * Format số tiền theo chuẩn Việt Nam
 * @param {number} value - Giá trị cần format
 * @returns {string} - Chuỗi đã format
 */
function formatCurrency(value) {
    if (!value || value === 0) return '0 đ';
    return new Intl.NumberFormat('vi-VN', { 
        style: 'currency', 
        currency: 'VND' 
    }).format(value);
}

/**
 * Format ngày tháng theo chuẩn Việt Nam
 * @param {string} dateString - Chuỗi ngày (YYYY-MM-DD)
 * @returns {string} - Ngày đã format (DD/MM/YYYY)
 */
function formatDate(dateString) {
    if (!dateString || dateString === '') return '';
    try {
        const date = new Date(dateString);
        return date.toLocaleDateString('vi-VN');
    } catch {
        return dateString;
    }
}

/**
 * Format số thập phân
 * @param {number} value - Giá trị cần format
 * @param {number} decimals - Số chữ số thập phân
 * @returns {string} - Chuỗi đã format
 */
function formatNumber(value, decimals = 2) {
    if (!value || value === 0) return '0';
    return new Intl.NumberFormat('vi-VN', {
        minimumFractionDigits: decimals,
        maximumFractionDigits: decimals
    }).format(value);
}
