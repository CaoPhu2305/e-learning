// Đặt trong file .js hoặc trong thẻ <script>
$(document).ready(function () {

    // Khi bấm vào một mục trong bảng câu hỏi
    $('.palette-item').on('click', function () {

        // Lấy số thứ tự câu hỏi từ mục được bấm
        // (Giả sử bạn có <div class="palette-item" data-question-num="5">5</div>)
        var questionNumToShow = $(this).data('question-num'); // Ví dụ: 5

        // 1. Ẩn TẤT CẢ các câu hỏi đi
        $('.question-container').hide();

        // 2. Chỉ hiện câu hỏi được bấm
        $('#question-' + questionNumToShow).show();

        // 3. Cập nhật trạng thái "current" trên bảng câu hỏi
        $('.palette-item').removeClass('current');
        $(this).addClass('current');
    });

    // Xử lý nút "Trang tiếp"
    $('#next-btn').on('click', function () {
        // Viết code để tìm câu hiện tại, ẩn nó đi và hiện câu tiếp theo
    });

});