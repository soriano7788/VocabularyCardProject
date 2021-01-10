<template>
  <div class="row">
    <div class="d-grid gap-2 d-md-block">
      <span>{{ cardSetName }}</span>
      <button class="btn btn-danger btn-sm" @click="removeCardSet">
        刪除單字集
      </button>
    </div>
    <div
      class="row row-cols-1 row-cols-md-2 g-3 gx-5 px-5 justify-content-center"
    >
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
  props: ["cardSet"],
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
    currentCardSetId: function() {
      return this.$route.params.cardSetId;
    },
    cardSetName: function() {
      return this.$store.getters.currentCardSetName;
    },
  },
  methods: {
    removeCardSet: function() {
      if (confirm("確定刪除?")) {
        this.$store.dispatch("removeCardSet", this.currentCardSetId);
      }
    },
  },
  created: function() {
    this.$store.dispatch("fetchCardSetAllCards", this.$route.params.cardSetId);
    this.$store.dispatch("fetchCardSetName", this.$route.params.cardSetId);
  },
  destroyed: function() {
    //clear cards
    this.$store.dispatch("clearCards");
  },
};
</script>
