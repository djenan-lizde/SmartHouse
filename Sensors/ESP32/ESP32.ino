#include "DHT.h"
#include "ArduinoJson.h"
#include <Servo.h>
#include <WiFi.h>
#include <HTTPClient.h>
#include <WiFiClientSecure.h>

#define DHTPIN 14
#define DHTTYPE DHT11   // DHT 11

int Gas_analog = 4;    // used for ESP32
int servoPin = 26;
int pos = 0, humidity, heatIndex, temperatureFahrenheit, temperatureCelsius;  // variable to store the servo position

const char* ssid     = "Trio Fantastico";
const char* password = "thisisapassword2019$";

DHT dht(DHTPIN, DHTTYPE);
Servo servo1;
HTTPClient httpsClient;

void setup() {
  Serial.begin(115200);

  // We start by connecting to a WiFi network

  Serial.println();
  Serial.println();
  Serial.print("Connecting to ");
  Serial.println(ssid);

  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }

  Serial.println("");
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());

  servo1.attach(servoPin);
  servo1.write(0);
  dht.begin();
}

void loop() {
  // Wait a few seconds between measurements.
  delay(2000);

  temperature();
  gas();

  if (WiFi.status() == WL_CONNECTED) {

    httpsClient.begin("http://07cf14517b45.ngrok.io/api/temperatures/" + String(temperatureCelsius) + "/" + String(temperatureFahrenheit) + "/" + String(humidity) + "/" + String(heatIndex)); //Specify destination for HTTP request

    httpsClient.GET();
  }
}

void openWindow() {
  for (int posDegrees = 0; posDegrees <= 180; posDegrees++) {
    servo1.write(posDegrees);
    delay(20);
  }
}

void closeWindow() {
  for (int posDegrees = 180; posDegrees >= 0; posDegrees--) {
    servo1.write(posDegrees);
    delay(20);
  }
}

void gas() {
  int gassensorAnalog = analogRead(Gas_analog);

  if (gassensorAnalog > 300) {
    openWindow();
    if (WiFi.status() == WL_CONNECTED) {

      httpsClient.begin("http://07cf14517b45.ngrok.io/api/sms/sendsms"); //Specify destination for HTTP request

      httpsClient.GET();
    }
    Serial.println(gassensorAnalog);
  }
  else if (gassensorAnalog == 0) {
    closeWindow();
    Serial.println(gassensorAnalog);
  }
}

void temperature() {
  // Reading temperature or humidity takes about 250 milliseconds!
  // Sensor readings may also be up to 2 seconds 'old' (its a very slow sensor)
  humidity = dht.readHumidity();
  temperatureCelsius = dht.readTemperature();
  temperatureFahrenheit = dht.readTemperature(true);
  heatIndex = dht.computeHeatIndex(temperatureCelsius, humidity, false);

  // Check if any reads failed and exit early (to try again).
  if (isnan(humidity) || isnan(temperatureCelsius) || isnan(temperatureFahrenheit) || isnan(heatIndex)) {
    Serial.println(F("Failed to read from DHT sensor!"));
    return;
  }

  StaticJsonDocument<1024> doc;
  doc["temperatureCelsius"] = temperatureCelsius;
  doc["temperatureFahrenheit"] = temperatureFahrenheit;
  doc["humidity"] = humidity;
  doc["heatIndex"] = heatIndex;

  serializeJsonPretty(doc, Serial);
}
