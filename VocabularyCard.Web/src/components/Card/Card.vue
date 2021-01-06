<template>
  <div>
    <p>{{ card.Id }}</p>
    <p>{{ card.Vocabulary }}</p>
    <p>{{ card.Creator }}</p>
  </div>
</template>

<script>
export default {
  props: ["card"],
  created: function() {
    console.log("hello");
  },
};
</script>
