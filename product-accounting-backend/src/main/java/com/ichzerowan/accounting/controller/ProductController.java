package com.ichzerowan.accounting.controller;

import com.ichzerowan.accounting.dao.ObjectNotFoundException;
import com.ichzerowan.accounting.dao.ProductRepository;
import com.ichzerowan.accounting.model.Product;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/products")
class ProductController {

    @Autowired
    private ProductRepository repository;

    @GetMapping("")
    List<Product> getAll(){
        return repository.findByArchived(false);
    }

    @GetMapping("/archived")
    List<Product> getArchived(){
        return repository.findByArchived(true);
    }

    @GetMapping("/{id}")
    Product getProduct(@PathVariable Long id){
        return repository.findById(id)
                .orElseThrow(() -> new ObjectNotFoundException(Product.class, id));
    }

    @PostMapping("")
    Product addProduct(@RequestBody Product newProduct){
        return repository.save(newProduct);
    }

    @PutMapping("/{id}")
    Product updateProduct(@RequestBody Product newProduct, @PathVariable Long id){
        return repository.findById(id).map(product -> {
            product.setName(newProduct.getName());
            product.setDescription(newProduct.getDescription());
            product.setPrice(newProduct.getPrice());
            return repository.save(product);
        }).orElseThrow(() -> new ObjectNotFoundException(Product.class, id));
    }

    @DeleteMapping("/{id}")
    Product deleteProduct(@PathVariable Long id){
        return repository.findById(id).map(product -> {
            product.setArchived(true);
            return repository.save(product);
        }).orElseThrow(() -> new ObjectNotFoundException(Product.class, id));
    }
}
