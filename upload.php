<?php
$username = "root"; 
$password = ""; 
$database = "users";
$mysqli = new mysqli("localhost", $username, $password, $database);
$response = array();
if($mysqli->connect_error)
{
   echo "Error In Server";
}
else{
    if(is_uploaded_file($_FILES["user_file"]["tmp_name"]) && $_POST["uuid"])
    {
        if(!is_dir("Files"))
            mkdir("Files");
        if(!is_dir("Files/". $_POST['uuid']))
            mkdir("Files/" . $_POST['uuid']);
        $tmp_file = $_FILES["user_file"]["tmp_name"];
        $file_name = $_FILES["user_file"]["name"];
        $upload_dir = "./Files/". $_POST['uuid'] . '/'.$file_name;
        $sql = "INSERT INTO users.files (uuid, filename) VALUES ('{$_POST['uuid']}', '{$file_name}')";
        if(move_uploaded_file($tmp_file, $upload_dir) && $mysqli -> query($sql))
        {
            echo "Upload Succeded";
        }
        else
        {
            echo "Upload Failed";
        }
    }
    else
    {
        echo "Invalid request";
    }
}
?>