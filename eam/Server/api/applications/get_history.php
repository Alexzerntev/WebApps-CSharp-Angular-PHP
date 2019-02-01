<?php
// required headers
header('Access-Control-Allow-Origin: *');
header("Content-Type: application/json; charset=UTF-8");
header("Access-Control-Allow-Methods: OPTIONS, POST");
header("Access-Control-Max-Age: 3600");
header("Access-Control-Allow-Headers: Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");

if ($_SERVER['REQUEST_METHOD'] == 'OPTIONS') {
    exit;
}

// files needed to connect to database
include_once '../config/database.php';

$database = new Database();
$db = $database->getConnection();

// required to decode jwt
include_once '../config/core.php';
include_once '../lib/php-jwt/src/BeforeValidException.php';
include_once '../lib/php-jwt/src/ExpiredException.php';
include_once '../lib/php-jwt/src/SignatureInvalidException.php';
include_once '../lib/php-jwt/src/JWT.php';
use \Firebase\JWT\JWT;

// get posted data
$data = json_decode(file_get_contents("php://input"));

// get jwt
$jwt = isset($data->jwt) ? $data->jwt : "";

// if jwt is not empty
if ($jwt) {

    // if decode succeed, show user details
    try {
        // decode jwt
        $decoded = JWT::decode($jwt, $key, array('HS256'));

    }
    // if decode fails, it means jwt is invalid
     catch (Exception $e) {

        // set response code
        http_response_code(401);

        // tell the user access denied  & show error message
        echo json_encode(array(
            "message" => "Access denied.",
            "error" => $e->getMessage(),
        ));
    }
}

$query = "SELECT DISTINCT
 application.date,lesson.title,books.title as book_title,books.author
 FROM application,lesson,books,application_lessons
 WHERE application_lessons.application_id = application.id and application_lessons.lesson_id = lesson.id AND application_lessons.book_id = books.id AND application.user_id = " . $decoded->data->id;

// prepare the query
$stmt = $db->prepare($query);
$stmt->execute();

$num = $stmt->rowCount();
$result = array();
if ($num > 0) {
    while ($row = $stmt->fetch(PDO::FETCH_ASSOC)) {
        $temp = array(
            "date" => $row['date'],
            "title" => $row['title'],
            "book_title" => $row['book_title'],
            "author" => $row['author'],
        );

        array_push($result, $temp);
    }

    http_response_code(200);
    echo json_encode(
        array(
            "data" => $result,
        )
    );

} else {
    // set response code
    http_response_code(400);

    // display message: unable to create user
    echo json_encode(array("error" => "Δεν έχετε δηλώσει κανένα μάθημα."));
}
