
public class Account {
	private int balance; 
	private int maxTransfer;
	
	public Account(int balance, int maxTransfer) { 
		this.balance = balance;
		this.maxTransfer = maxTransfer; 
	}
	
	public void setBalance(int balance) { 
		this.balance = balance; 
	}
	
	public void setMaxTransfer(int maxTransfer) { 
		this.maxTransfer = maxTransfer; 
	}
	
	public int getBalance() { 
		return this.balance; 
	}

	public int getMaxBalance() { 
		return this.maxTransfer; 
	}
	
	public void withdraw(int amount) throws InSufficientFundException {
		if(amount > this.balance) { 
			throw new InSufficientFundException("Current balance is less than requested amount"); 
		} 
		else { 
			this.balance -= amount; 
		} 
	}
	
	public void transfer(int amount, int accNo) throws MaximumFundTransferException {
		if(amount > this.balance) { 
			throw new MaximumFundTransferException("Requested amount exceeds maximum fund transfer amount"); 
		} 
		else { 
			this.balance -= amount;
		} 
	}
}

public class InSufficientFundException extends Exception { 
	public InSufficientFundException(String msg) { 
		super(msg); 
	} 
}

public class MaximumFundTransferException extends Exception {
	public MaximumFundTransferException(String msg) {
		super(msg); 
	} 
}


public class TestAccount {
	public static void main(String[] args) {
		Account acc1 = new Account(1000,10000); 
		Account acc2 = new Account(1000,10000); 
		
		try { 
			System.out.println("Current balance : " + acc1.getBalance()); 
			System.out.println("Withdrawing Rs 200"); 
			acc1.withdraw(200); 
		
			System.out.println("Current balance : " + acc1.getBalance());
			System.out.println("Withdrawing Rs 1000"); 
			acc1.withdraw(1000); 
		} 
		
		catch(InSufficientFundException e) {
			System.out.println(e); 
		} 
		
		System.out.println(""); 
		
		try {
			System.out.println("Current balance : " + acc2.getBalance());
			System.out.println("Withdrawing Rs 200"); 
			acc2.withdraw(200); 
			System.out.println("Current balance : " + acc2.getBalance()); 
			System.out.println("Withdrawing Rs 700"); 
			acc2.withdraw(700); System.out.println("Current balance : " + acc2.getBalance()); 
			System.out.println("Transferring Rs 11000"); 
			acc2.transfer(11000, 1234); 
		} 
		catch(InSufficientFundException | MaximumFundTransferException e) {
			System.out.println(e); 
		} 
	} 
}
