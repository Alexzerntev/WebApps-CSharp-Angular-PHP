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

// get posted data
$department_id = json_decode(file_get_contents("php://input"));

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

$query = "INSERT INTO application
SET
user_id = " . $decoded->data->id;

// prepare the query
$stmt = $db->prepare($query);
$stmt->execute();

$last_id = $db->lastInsertId();

foreach ($data->array as &$value) {
    $query = "INSERT INTO application_lessons
    SET
    application_id = " . $last_id . ", lesson_id = " . $value->lesson_id . ", book_id = " . $value->book_id;

// prepare the query
    $stmt = $db->prepare($query);
    $stmt->execute();
}
// $arr is now array(2, 4, 6, 8)
unset($value);

http_response_code(200);
