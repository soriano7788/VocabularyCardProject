<template>
  <div class="row justify-content-center">
    <div class="d-grid gap-2 d-md-block">
      <span class="fs-1">{{ cardSetName }}</span>
      <i
        id="create-card-btn"
        @click="createCard"
        class="bi bi-plus-circle-fill position-fixed"
        :title="$t('CARD.ADD_CARD')"
      ></i>
      <button
        class="btn btn-secondary btn-sm position-absolute"
        @click="removeCardSet"
        style="right:10px;"
      >
        {{ $t("CARD_SET.DELETE_CARD_SET") }}
      </button>
    </div>
    <!-- <div class="row row-cols-1 row-cols-md-2 g-3 gx-5 px-5"> -->
    <div class="row row-cols-1 row-cols-md-2 gx-5">
      <card
        v-for="card in cards"
        v-bind:key="card.Id"
        v-bind:card="card"
        @showEditCard="editCard($event)"
      ></card>
    </div>
    <div v-if="showCreateCardForm" class="row justify-content-center">
      <create-card
        :cardSetId="currentCardSetId"
        @closePanel="showCreateCardForm = false"
      ></create-card>
    </div>
    <div v-if="showEditCardForm" class="mt-3 row justify-content-center">
      <edit-card
        :card="currentEditCard"
        @closeEditPanel="showEditCardForm = false"
      ></edit-card>
    </div>
  </div>
</template>

<script>
import Card from "./Card.vue";
import CreateCard from "./CreateCard.vue";
import EditCard from "./EditCard.vue";

export default {
  props: ["cardSet"],
  components: {
    card: Card,
    createCard: CreateCard,
    editCard: EditCard,
  },
  data: function() {
    return {
      mode: "card", // card or paper
      showCreateCardForm: false,
      showEditCardForm: false,
      currentEditCard: null,
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
    editedCard: function() {
      this.$store.getters.cards.find();
    },
  },
  methods: {
    removeCardSet: function() {
      if (confirm("確定刪除?")) {
        this.$store.dispatch("removeCardSet", this.currentCardSetId);
      }
    },
    createCard: function() {
      this.showCreateCardForm = !this.showCreateCardForm;
    },
    editCard: function(card) {
      this.currentEditCard = this.generateCloneCard(card);
      this.showEditCardForm = true;
    },
    generateCloneCard: function(card) {
      const cloneCard = Object.assign({}, card);
      const cloneInterprets = [];
      for (let i = 0; i < cloneCard.Interpretations.length; i++) {
        const cloneInterpret = Object.assign({}, cloneCard.Interpretations[i]);
        cloneInterprets.push(cloneInterpret);
      }
      cloneCard.Interpretations = cloneInterprets;
      return cloneCard;
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

<style scoped>
#create-card-btn {
  font-size: 350%;
  right: 10px;
  bottom: 10%;
  cursor: pointer;
}
</style>
