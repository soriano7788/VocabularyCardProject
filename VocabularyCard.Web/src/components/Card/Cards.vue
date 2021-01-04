<template>
  <div class="row"></div>
</template>

<script>
export default {
  props: ["cards", "cardSetId"],
  data: function() {
    return {
      mode: "card", // card or paper
    };
  },
  created: function() {
    // 不知道這個時間點是否能取得 props 的 cardSetId
    this.$store.dispatch("getCardsByCardSetId", this.cardSetId);
  },
};
</script>
