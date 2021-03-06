import React from 'react';
import { View, Text, StyleSheet } from 'react-native';

const DetailsScreen = () => {
    return (
      <View style={styles.container}>
        <Text>Details Screen</Text>
      </View>
    );
};

export default DetailsScreen;

const styles = StyleSheet.create({
  container: {
    flex: 1, 
    alignItems: 'center', 
    justifyContent: 'center'
  },
});