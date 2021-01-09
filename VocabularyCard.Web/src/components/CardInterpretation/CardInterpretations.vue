<template>
  <ul class="list-group list-group-flush interpret-block">
    <card-interpretation
      v-for="interpretation in interpretations"
      v-bind:key="interpretation.Id"
      v-bind:interpretation="interpretation"
    ></card-interpretation>
  </ul>
</template>

<script>
import CardInterpretation from "./CardInterpretation.vue";

export default {
  components: {
    cardInterpretation: CardInterpretation,
  },
  props: ["interpretations"],
};
</script>

<style scoped>
.interpret-block {
  text-align: left;
}
</style>
