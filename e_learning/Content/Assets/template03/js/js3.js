$(document).ready(function () {
    var questionContainer = $('#question-list-container');

    // === HÀM THÊM CÂU HỎI ===
    $('#add-question-btn').on('click', function () {
        // Lấy index mới (bằng số lượng câu hỏi hiện tại)
        var newIndex = questionContainer.find('.question-editor-block').length;
        var newNumber = newIndex + 1;

        // Lấy HTML từ template
        var template = $('#question-template').html();

        // Thay thế các placeholder __INDEX__ và __NUMBER__
        var newQuestionHtml = template.replace(/__INDEX__/g, newIndex)
            .replace(/__NUMBER__/g, newNumber);

        // Thêm câu hỏi mới vào cuối danh sách
        questionContainer.append(newQuestionHtml);
    });

    // === HÀM XÓA CÂU HỎI ===
    // Dùng event delegation vì các nút xóa được tạo động
    questionContainer.on('click', '.btn-delete-question', function () {
        // Tìm đến khối câu hỏi cha gần nhất và xóa nó
        $(this).closest('.question-editor-block').remove();

        // **QUAN TRỌNG: Đánh số lại (re-index) tất cả câu hỏi **
        // Việc này để đảm bảo MVC Binding không bị "lủng" index
        reindexQuestions();
    });

    // === HÀM ĐÁNH SỐ LẠI (RE-INDEX) ===
    function reindexQuestions() {
        questionContainer.find('.question-editor-block').each(function (newIndex) {
            var newNumber = newIndex + 1;
            var block = $(this);

            // 1. Cập nhật tiêu đề: "Câu X"
            block.find('.question-title').text('Câu ' + newNumber);

            // 2. Cập nhật data-index
            block.attr('data-index', newIndex);

            // 3. Cập nhật TẤT CẢ các thuộc tính 'name' và 'id'
            block.find('input, textarea').each(function () {
                var oldName = $(this).attr('name');
                if (oldName) {
                    var newName = oldName.replace(/Questions\[\d+\]/g, 'Questions[' + newIndex + ']');
                    $(this).attr('name', newName);
                }

                var oldId = $(this).attr('id');
                if (oldId) {
                    var newId = oldId.replace(/Questions\[\d+\]/g, 'Questions[' + newIndex + ']');
                    $(this).attr('id', newId);
                }

                // Cập nhật 'name' cho radio button (trường hợp đặc biệt)
                if ($(this).is(':radio')) {
                    var radioName = $(this).attr('name');
                    if (radioName && radioName.includes('CorrectAnswerIndex')) {
                        var newRadioName = radioName.replace(/Questions\[\d+\]\.CorrectAnswerIndex/g, 'Questions[' + newIndex + '].CorrectAnswerIndex');
                        $(this).attr('name', newRadioName);
                    }
                }
            });
        });
    }
});