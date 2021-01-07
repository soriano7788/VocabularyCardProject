<template>
  <div class="row">
    <h1>This is cards list</h1>
    <div class="row row-cols-1 row-cols-md-2 g-3">
      <card
        v-for="card in cards"
        v-bind:key="card.Id"
        v-bind:card="card"
      ></card>
    </div>
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
    this.$store.dispatch("fetchCardSetAllCards", this.$route.params.cardSetId);
  },
  destroyed: function() {
    //clear cards
    this.$store.dispatch("clearCards");
  },
};
</script>
