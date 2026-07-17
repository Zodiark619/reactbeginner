import { useEffect, useState } from "react";
import ProductList from "./components/ProductList";
import Header from "./Header";

const App = () => {
  return (
    <>
      <Header />
      <div>
        <h1>Products</h1>
        <ProductList />
        <div></div>
      </div>
    </>
  );
};

export default App;
