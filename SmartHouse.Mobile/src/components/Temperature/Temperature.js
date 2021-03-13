import React, { useState } from "react";
import {
  View,
  StyleSheet,
  ActivityIndicator,
  SafeAreaView,
} from "react-native";
import TemperatureForm from "./TemperatureForm";

const Temperature = () => {
  const [isLoading, setLoading] = useState(false);

  return (
    <SafeAreaView style={styles.container}>
      {isLoading ? <ActivityIndicator size="large" color="green" /> : null}
      <View style={styles.formContainer}>
        <TemperatureForm />
      </View>
    </SafeAreaView>
  );
};

export default Temperature;

const styles = StyleSheet.create({
  container: {
    flex: 1
  },
  formContainer: {
    marginBottom: 30,
  },
});
