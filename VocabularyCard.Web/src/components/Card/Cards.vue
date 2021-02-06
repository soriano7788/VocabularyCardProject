<template>
  <div class="row">
    <div class="d-grid gap-2 d-md-block">
      <h1>{{ cardSetName }}</h1>
      <button
        class="btn btn-primary btn-sm position-fixed"
        @click="createCard"
        style="right:10px; top:50%;"
      >
        新增單字卡
      </button>
      <button
        class="btn btn-danger btn-sm position-absolute"
        @click="removeCardSet"
        style="right:10px;"
      >
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
        @showEditCard="editCard($event)"
      ></card>
    </div>
    <div v-if="showCreateCardForm" class="mt-3 row justify-content-center">
      <div class="col-md-5">
        <create-card
          :cardSetId="currentCardSetId"
          @closePanel="showCreateCardForm = false"
        ></create-card>
      </div>
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
