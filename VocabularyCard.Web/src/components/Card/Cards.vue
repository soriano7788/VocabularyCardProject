<template>
  <div class="row">
    <h1>This is cards list</h1>
    <card v-for="card in cards" v-bind:key="card.Id" v-bind:card="card"></card>
    <!-- <card></card> -->
  </div>
</template>

<script>
import Card from "./Card.vue";

export default {
  //   props: ["cards", "cardSetId"],
  components: {
    card: Card,
  },
  data: function() {
    return {
      mode: "card", // card or paper
    };
  },
  computed: {
    cards: function() {
      return this.$store.getters.cards;
    },
  },
  created: function() {
    // this.$store.dispatch("fetchCardSetAllCards", this.$router.params.cardSetId);
    this.$store.dispatch("fetchCardSetAllCards", 1);
  },
};
</script>
