#import "iOSNativeReview.h"
#import <StoreKit/StoreKit.h>

@implementation iOSNativeReview {
}

# pragma mark - C API

bool requestReview() {
    if([UIDevice currentDevice].systemVersion.floatValue >= 10.3) {
        [SKStoreReviewController requestReview];
        return true;
    }
    else
    {
        return false;
    }
}
@end
