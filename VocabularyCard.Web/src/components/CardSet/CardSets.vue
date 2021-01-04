<template>
  <div class="row">
    <!-- 這是 單字集 清單 -->
    <h1>CardSets List</h1>
    <card-set
      v-for="cardSet in cardSets"
      v-bind:key="cardSet.Id"
      v-bind:cardSet="cardSet"
    ></card-set>
    <!-- <div v-for="cardSet in cardSets" :key="cardSet.Id">
      <h1>display name: {{ cardSet.DisplayName }}</h1>
    </div> -->
  </div>
</template>

<script>
import CardSet from "./CardSet.vue";

export default {
  components: {
    cardSet: CardSet,
  },
  // data: function() {
  //   return {
  //     cardSets: [],
  //   };
  // },
  computed: {
    cardSets: function() {
      return this.$store.getters.cardSets;
    },
  },
  methodds: {
    // getAllCardSets: function() {
    //   this.cardSets = this.$store.dispatch("getAllCardSets");
    // },
  },
  created: function() {
    this.$store.dispatch("getAllCardSets");
  },
};
</script>
