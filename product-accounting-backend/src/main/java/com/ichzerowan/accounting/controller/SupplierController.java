package com.ichzerowan.accounting.controller;

import com.ichzerowan.accounting.dao.ObjectNotFoundException;
import com.ichzerowan.accounting.dao.SupplierRepository;
import com.ichzerowan.accounting.model.supplier.Supplier;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/suppliers")
public class SupplierController {

    @Autowired
    private SupplierRepository repository;

    @GetMapping("")
    List<Supplier> getAll(){
        return repository.findByArchived(false);
    }

    @GetMapping("/archived")
    List<Supplier> getArchived(){
        return repository.findByArchived(true);
    }

    @GetMapping("/{id}")
    Supplier getSupplier(@PathVariable Long id){
        return repository.findById(id)
                .orElseThrow(() -> new ObjectNotFoundException(Supplier.class, id));
    }

    @PostMapping("")
    Supplier addSupplier(@RequestBody Supplier newSupplier){
        return repository.save(newSupplier);
    }

    @PutMapping("/{id}")
    Supplier updateSupplier(@RequestBody Supplier newSupplier, @PathVariable Long id){
        return repository.findById(id).map(supplier -> {
            supplier.setName(newSupplier.getName());
            supplier.setPhone(newSupplier.getPhone());
            return repository.save(supplier);
        }).orElseThrow(() -> new ObjectNotFoundException(Supplier.class, id));
    }

    @DeleteMapping("/{id}")
    Supplier deleteSupplier(@PathVariable Long id){
        return repository.findById(id).map(product -> {
            product.setArchived(true);
            return repository.save(product);
        }).orElseThrow(() -> new ObjectNotFoundException(Supplier.class, id));
    }
}
