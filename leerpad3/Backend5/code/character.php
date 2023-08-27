<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Character </title>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
    <link href="resources/css/style.css" rel="stylesheet"/>
</head>
<body>



<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "tast";

try {
    $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
} catch(PDOException $e) {
    echo "Fout bij de databaseverbinding: " . $e->getMessage();
}


if (isset($_GET['name'])) {
    $name = $_GET['name'];
    
    try {
        $stmt = $conn->prepare("SELECT * FROM characters WHERE name = :name");
        $stmt->bindParam(':name', $name);
        $stmt->execute();
        $character = $stmt->fetch(PDO::FETCH_ASSOC);
        
        if (!$character) {
            echo "Karakter niet gevonden";
            exit;
        }
    } catch(PDOException $e) {
        echo "Fout bij het ophalen van het karakter: " . $e->getMessage();
    }
} else {
    echo "Geen karakternaam opgegeven";
    exit;
}
?>



<header><h1><?php echo $character['name']; ?></h1>
    <a class="backbutton" href="index.php"><i class="fas fa-long-arrow-alt-left"></i> Terug</a></header>
    

<div id="container">
    <div class="detail">
        <div class="left">
        <img class="avatar" src="resources/images/<?php echo $character['avatar']; ?>">

        <div class="stats" style="background-color: <?php echo $character['color']; ?>">

    <ul class="fa-ul">
        <li><span class="fa-li"><i class="fas fa-heart"></i></span> <?php echo $character['health']; ?></li>
        <li><span class="fa-li"><i class="fas fa-fist-raised"></i></span> <?php echo $character['attack']; ?></li>
        <li><span class="fa-li"><i class="fas fa-shield-alt"></i></span> <?php echo $character['defense']; ?></li>
    </ul>
    <ul class="gear">
        <li><b>Weapon</b>: <?php echo $character['weapon']; ?></li>
        <li><b>Armor</b>: <?php echo $character['armor']; ?></li>
    </ul>

            </div>
        </div>
        <div class="right">
            <p>
    <?php echo $character['bio']; ?>
            </p>
        </div>
        <div style="clear: both"></div>
    </div>
</div>
<footer>&copy; Hamid 2023</footer>
</body>
</html>